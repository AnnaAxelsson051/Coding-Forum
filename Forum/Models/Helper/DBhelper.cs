using System;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace Forum.Models.Helper
{
	public class DBhelper
	{
        private readonly DataContext _context;

        public DBhelper(DataContext context)
        {
            _context = context;
        }

        // retrieves a list of topics from the database and maps
        // them to a list of TopicViewModel objects, returning
        // their IDs and titles
       
        public List<TopicViewModel> LoadTopics()
        {
            var topics = _context.Topic
                .Select(topic => new TopicViewModel
              {
                    Id = topic.Id,
                   Title = topic.Title,
                }
            )
            .ToList();

            return topics;
        }

        // retrieves a list of threads associated with a given subjectId
        // and joins them with their corresponding topics, returning
        // their thread IDs, headings, and topic reference IDs in a
        // TopicThreadViewModel format

        public List<TopicThreadViewModel> LoadThreads(int topicId)
        {
            var threadsAndTopics = _context.Thread
                                    .Where(thread => thread.TopicReferenceId == topicId)
                                    .Join(
                                        _context.Topic,
                                        thread => thread.TopicReferenceId,
                                        topic => topic.Id,
                                        (thread, topic) => new TopicThreadViewModel
                                        {
                                           
                                            ThreadId = thread.Id,
                                            ThreadHeading = thread.Heading,
                                            TopicReferenceId = topic.Id,
                                        }
                                    )
                                    .ToList(); 

            return threadsAndTopics;
        }

        // retrieves a list of posts and their associated thread information
        // for a given thread ID joins the posts with their respective
        // threads to form a combined ThreadPostViewModel,
        // and returns the combined list
        public List<ThreadPostViewModel> LoadPosts(int threadId)
        {
            var postAndThread = _context.Post
                                    .Where(post => post.ThreadReferenceId == threadId)
                                    .Join(
                                        _context.Thread,
                                        post => post.ThreadReferenceId,
                                        thread => thread.Id,
                                        (post, thread) => new ThreadPostViewModel
                                        {
                                           
                                            PostId = thread.Id,
                                            PostTitle = post.Title,
                                            TextBody = post.TextBody,
                                            ThreadReferenceId = thread.Id,
                                            ThreadHeading = thread.Heading,
                                        }
                                    )
                                    .ToList(); 

            return postAndThread;
        }

        // Retrieves a post by its postId from the db, updates its 
        // textBody and optionally its title, and then saves the changes to db

        public void EditPost(int postId, string textBody, string? title)
        {
            Post post = (from item in _context.Post
                         where item.Id == postId
                         select item).First();
            post.TextBody = textBody;
            if (title != null)
                post.Title = title;
            _context.SaveChanges();
        }

        // Updates the heading of a specific thread in db
        public void EditThread(int threadId, string heading)
        {
            Models.Thread thread = (from item in _context.Thread
                                    where item.Id == threadId
                                    select item).First();
            thread.Heading = heading;
            _context.SaveChanges();
        }

        // Updates the title of a specific topic in db
        public void EditTopic(int topicId, string title)
        {
            Topic topic = (from item in _context.Topic
                               where item.Id == topicId
                               select item).First();
            topic.Title = title;
            _context.SaveChanges();
        }

        public void AddPost(Post newpost)
        {
            _context.Post.Add(newpost);
            _context.SaveChanges();
        }
        public void AddThread(Models.Thread newThread)
        {
            _context.Thread.Add(newThread);
            _context.SaveChanges();
        }

        // Adds a new thread and its associated post to db
        public void AddThreadPost(Models.Thread newThread, Post newThreadPost)
        {
            newThread.Posts = new List<Post> { newThreadPost };
            newThreadPost.Thread = newThread;

            _context.Post.Add(newThreadPost);
            _context.Thread.Add(newThread);
            _context.SaveChanges();
        }
    }
}

