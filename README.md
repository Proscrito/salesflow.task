# salesflow.task
Usually I reject test tasks. But this was even funny excercize.

I didn't spend time for creating production-level code because of time limitations. Tried to show the approach only.

1st and 2nd I implemented in the class library and created couple of unit tests to run & debug the code. And I made simple console application to run and test 3rd task code. See demo.png for testing results.


1. Nuff said, 10 sec to fresh mi mind with some googling and 2 minutes for implementation.
2. I would not use custom deserializer in production for this solution, I would probably make a transformation step to build hierarchical structure and then deserialize it with any Json library. In the task bounds I just made some custom tweaks for the Newtonsoft.Json. Current implementation would deserialize any kind of hierarchical items if all the items have corresponding classes for deserialization.
3. The heart of the implementation is TaskQueue service to get the next available task from the database. It calculates the remaining delay within the client and keeps the schedule consistent. It can be improved with cahche to decrease database roundtrips, I didn't spend time for optimization. This solution works with any number of parallel workers.

In AWS I would use single publisher service to pull tasks from DB and push them to the queue. Currently running ClientId should be cached to prevent processing the same client tasks at the same time. No-brain worker to pick up task from the queue and process it. This way we will keep a single task per client in the queue. And we need an endpoint to get the worker feedback after the task is completed.failed.
