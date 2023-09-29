using System;
namespace Forum.Models.Helper
{
	public class DBhelper
	{
        private readonly DataContext _context;

        public DBhelper(DataContext context)
        {
            _context = context;
        }

        public void AddPost(Post newpost)
        {
            _context.Post.Add(newpost);
            _context.SaveChanges();
        }

        // retrieves a list of topics from the database and maps
        // them to a list of TopicViewModel objects, returning
        // their IDs and titles
       
        public List<TopicViewModel> LoadTopics()
        {
            var subjects = _context.Topic
                .Select(subject => new TopicViewModel
                {
                    Id = subject.Id,
                    Title = subject.Title,
                }
            )
            .ToList();

            return subjects;
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

        public void AddThreadPost(Models.Thread newThread, Post newThreadPost)
        {
            // Set the relationship between Thread and Post
            newThread.Posts = new List<Post> { newThreadPost };
            newThreadPost.Thread = newThread;

            _context.Post.Add(newThreadPost);
            _context.Thread.Add(newThread);
            _context.SaveChanges();
        }
    }
}

