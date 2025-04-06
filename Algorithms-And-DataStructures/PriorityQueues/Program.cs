using PriorityQueues;

//PriorityQueueTester.Tester.Run();
//PriorityQueueTester.Tester.TimeMeasure();
var pq = new MyPriorityQueue<int,int>();
pq.Enqueue(-6, -6);
pq.Enqueue(6, 6);
pq.Enqueue(-2, -2);
pq.Enqueue(-2, -2);
pq.Enqueue(-1, -1);
pq.Enqueue(6, 6);
pq.Enqueue(-1, -1);
pq.Enqueue(-6, -6);

var c = pq.Count;

for (int i = 0; i < c; i++)
{ 
    Console.WriteLine(pq.Dequeue());
}


