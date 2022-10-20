// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

int[,] a;
int[,] b;
void Main()
    {
        a = new int[10000, 10000];
        b = new int[10000, 10000];
        initializeMatrixes();
        Stopwatch sw = new Stopwatch();

    Console.WriteLine("Matrix Size: 100000");
    Console.WriteLine("Starting Parallel Multiplication");
    sw.Start();
    int[,] d = MultiplyMatrixParallel(a, b);
    sw.Stop();
    Console.WriteLine("Time = {0:f5} seconds", sw.ElapsedMilliseconds / 1000d);
    Console.WriteLine("Starting Sequential Multiplication");
    sw.Start();
    int[,] c = MultiplyMatrixSequential(a, b);
    sw.Stop();
    Console.WriteLine("Time = {0:f5} seconds", sw.ElapsedMilliseconds / 1000d);

 


}

void initializeMatrixes()
{
    Random random = new Random(10);
    for(int i = 0; i < a.GetLength(0); i++)
    {
        for(int j = 0; j < b.GetLength(0); j++)
        {
            a[i, j] = random.Next();
            b[i, j] = random.Next();
        }
    }
}

static int[,] MultiplyMatrixSequential(int[,] a, int[,] b)
{
    int[,] c = new int [a.GetLength(0), b.GetLength(1)];
    for (int i = 0; i < a.GetLength(0); i++)
    {
        for (int j = 0; j < b.GetLength(1); j++)
        {
            c[i, j] = 0;
            for (int k = 0; k < a.GetLength(1); k++)
            {
                c[i, j] += a[i, k] * b[k, j];
            }
        }
    }
    return c;
}
static int[,] MultiplyMatrixParallel(int[,] a, int[,] b)
{

    int[,] c = new int[a.GetLength(0), b.GetLength(1)];

    Parallel.For(0, a.GetLength(0), i =>
    {
        for (int j = 0; j < b.GetLength(1); ++j)
            for (int k = 0; k < a.GetLength(1); ++k)
                c[i, j] += a[i, k] * b[k, j]; 
    }
    );

    return c;
}
Main();