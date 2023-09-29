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

        public List<TopicThreadViewModel> LoadThreads(int subjectId)
        {
            var threadsAndSubjects = _context.Thread
                                    .Where(thread => thread.TopicReferenceId == subjectId)
                                    .Join(
                                        _context.Topic,
                                        thread => thread.TopicReferenceId,
                                        subject => subject.Id,
                                        (thread, subject) => new TopicThreadViewModel
                                        {
                                           
                                            ThreadId = thread.Id,
                                            ThreadHeading = thread.Heading,
                                            TopicReferenceId = subject.Id,
                                        }
                                    )
                                    .ToList(); 

            return threadsAndSubjects;
        }
    }
}

