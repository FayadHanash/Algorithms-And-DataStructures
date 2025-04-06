using System;
using System.Collections.Generic;


namespace PriorityQueues
{
    class PriorityQueueTester
    {
        /// <summary>
        /// intializes a field(tester) of PriorityQueueTester 
        /// </summary>
        static PriorityQueueTester? tester;

        /// <summary>
        /// Property related tester field that creates and returns a new instance of PriorityQueueTester
        /// </summary>
        public static PriorityQueueTester? Tester 
        {
            get { if (tester == null) tester = new PriorityQueueTester(); return tester; }
        }

        /// <summary>
        /// Method that returns a string by random
        /// </summary>
        /// <param name="iter"></param>
        /// <returns>string</returns>
        string GetRandomString(int iter)
        {
            Random rnd = new Random();
            string str = "ABCDEFGHIJKLMNOPQRSTUVWYZ";
            string res = "";
            for (int i = 0; i < iter; i++)
            {
                int n = rnd.Next(str.Length);
                res = res + str[n];
            }
            return res;
        }
        /// <summary>
        /// Method that Measure the time of adding and removing elements from the queue
        /// </summary>
        public void TimeMeasure()
        {
            
            Random rnd = new Random();

            DateTime enqueueStart = DateTime.Now;
            var i = new MyPriorityQueue<string, int>();
            for (int j = 0; j < 100000; j++)
            {
                i.Enqueue(GetRandomString(7), rnd.Next(1, 10000));
            }
            DateTime enqueueEnd = DateTime.Now;

            
            
            DateTime dequeueStart = DateTime.Now;
            for (int j = 0; j < 100000; j++)
            {
                //Console.WriteLine($"Dequeue: {i.Dequeue().ToString()}");
                i.Dequeue();
            }

            DateTime dequeueEnd = DateTime.Now;

            Console.WriteLine("Enqueue starting...");
            long enqueueElapsedTicks = enqueueEnd.Ticks - enqueueStart.Ticks;
            TimeSpan enqueueElapsedSpan = new TimeSpan(enqueueElapsedTicks);
            Console.WriteLine($"minutes:({enqueueElapsedSpan.Minutes}), seconds:({enqueueElapsedSpan.Seconds}), milliseconds:({enqueueElapsedSpan.Milliseconds}).");
            Console.WriteLine($"Total ms {(enqueueElapsedSpan.Minutes * 6000) +(enqueueElapsedSpan.Seconds * 1000) + enqueueElapsedSpan.Milliseconds}");
            Console.WriteLine("Enqueue Ending...\n");

            Console.WriteLine("Dequeue starting...");
            long dequeueElapsedTicks = dequeueEnd.Ticks - dequeueStart.Ticks;
            TimeSpan dequeueElapsedSpan = new TimeSpan(dequeueElapsedTicks);
            Console.WriteLine($"minutes:({dequeueElapsedSpan.Minutes}), seconds:({dequeueElapsedSpan.Seconds}), milliseconds:({dequeueElapsedSpan.Milliseconds}).");
            Console.WriteLine($"Total ms {(dequeueElapsedSpan.Minutes * 6000) + (dequeueElapsedSpan.Seconds * 1000) + dequeueElapsedSpan.Milliseconds}");
            Console.WriteLine("Dequeue Ending...");
        }

        /// <summary>
        /// Method that tests the queue implementaion
        /// </summary>
        public void Run()
        {
            string x, y1;
            int y, x1;
            Random rnd = new Random();
            var i = new MyPriorityQueue<string, int>();
            try
            {
                for (int j = 0; j < 100; j++)
                {
                    i.Enqueue(GetRandomString(7), rnd.Next(1, 100));
                }
                i.TryPeek(out x, out y);
                Console.WriteLine($"Peek: {x} {y}");
                for (int j = 0; j < 100; j++)
                {
                    Console.WriteLine($"Dequeue: {i.Dequeue().ToString()}");
                }

                for (int j = 0; j < 5; j++)
                {
                    i.Enqueue(GetRandomString(3), rnd.Next(1, 100));
                }
                for (int j = 0; j < 5; j++)
                {
                    i.TryDequeue(out x, out y);
                    Console.WriteLine($"TryDequeue: {x} {y}");
                }
                i.TryDequeue(out x, out y);
                Console.WriteLine($"TryDequeueDefault: {x} {y}");
                i.Enqueue("A", 7);
                i.Enqueue("B", 3);
                i.Enqueue("C", 9);
                i.DisplayQueue();
                Console.WriteLine($"DequeueAt {i.DequeueAt("A", 7)}");
                i.DisplayQueue();
                var o = new Item<string, int>("A", 7);
                Console.WriteLine($"IncreasePriority {i.IncreasePriority("B", 3, 5)}");
                Console.WriteLine($"DecreasePriority {i.DecreasePriority("C", 9, 8)}");
                i.DisplayQueue();


                var s = new MyPriorityQueue<int, string>();
                for (int j = 0; j < 100; j++)
                {
                    s.Enqueue(rnd.Next(1, 100), GetRandomString(2));
                }
                s.TryPeek(out x1, out y1);
                Console.WriteLine($"Peek: {x1} {y1}");
                for (int j = 0; j < 100; j++)
                {
                    Console.WriteLine($"Dequeue: {s.Dequeue().ToString()}");
                }

                for (int j = 0; j < 5; j++)
                {
                    s.Enqueue(rnd.Next(1, 100), GetRandomString(3));
                }
                for (int j = 0; j < 5; j++)
                {
                    s.TryDequeue(out x1, out y1);
                    Console.WriteLine($"TryDequeue: {x1} {y1}");
                }
                s.TryDequeue(out x1, out y1);
                Console.WriteLine($"TryDequeueDefault: {x1} {y1}");
                s.Enqueue(7, "A");
                s.Enqueue(3, "B");
                s.Enqueue(9, "M");
                s.DisplayQueue();
                Console.WriteLine($"DequeueAt {s.DequeueAt(7, "A")}");
                s.DisplayQueue();
                var o1 = new Item<int, string>(7, "A");
                Console.WriteLine($"IncreasePriority {s.IncreasePriority(3, "B", "D")}");
                Console.WriteLine($"DecreasePriority {s.DecreasePriority(9, "M", "C")}");
                s.DisplayQueue();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }
    }
}
