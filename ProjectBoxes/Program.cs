using System;
using System.IO;
using System.Diagnostics;

namespace ProjectBoxes
{
    public class Program
    {
        public static string folderPath = Directory.GetCurrentDirectory();
        public class Box
        {
            public int width;
            public int length;
            public Box(int w, int l)
            {
                width = w;
                length = l;
            }
            public Box(Box b)
            {
                width = b.width;
                length = b.length;
            }
            public override string ToString()
            {
                return $"({width},{length})";
            }
        }
        public static void LDS(Box[] Boxes, string fileNumber)
        {
            string outputfile = Directory.GetCurrentDirectory() + "\\output" + fileNumber + ".txt";

            using (StreamWriter outputFile = new StreamWriter(outputfile))
            {
                int n = Boxes.Length;
                int[] Parents = new int[n];
                int[] Z = new int[n];

                //initialize Parent and Index arrays referred as P and I in the document
                //Parent is used to keep track of the subsequence
                //Z used to keep the length of the subsequences
                for (int i = 0; i < n; i++)
                {
                    Z[i] = 1;
                    Parents[i] = -1;
                }

                for(int i=1; i<n; i++)
                {
                    for(int j=0; j<i; j++)
                    {
                        if (Boxes[i].length < Boxes[j].length && Boxes[i].width != Boxes[j].width && Z[i] < Z[j] + 1)
                        {
                            Z[i] = Z[j] + 1;
                            Parents[i] = j;
                        }
                            
                    }
                }
                
                int length = Z[0], parentIndex = 0;
                for (int i = 0; i < n; i++)
                {
                    if (Z[i] > length)
                    {
                        length = Z[i];
                        parentIndex = i;
                    }
                }

                //write sequence itself to the output file
                outputFile.WriteLine(length.ToString());

                while (parentIndex != -1)
                {
                    outputFile.WriteLine(Boxes[parentIndex].ToString());
                    parentIndex = Parents[parentIndex];
                }

                outputFile.Close();
            }
        }
        public static void Merge(Box[] Boxes, int p, int q, int r)
        {
            int leftArraySize = q - p + 1;
            int rightArraySize = r - q;
            int rightIndex = 0, leftIndex = 0;

            Box[] LeftArray = new Box[leftArraySize];
            Box[] RightArray = new Box[rightArraySize];

            for (int i = 0; i < leftArraySize; i++)
            {
                LeftArray[i] = new Box(Boxes[p + i]);
            }

            for (int j = 0; j < rightArraySize; j++)
            {
                RightArray[j] = new Box(Boxes[q + j + 1]);
            }
            for (int k = p; k <= r; k++)
            {
                if (LeftArray.Length > leftIndex && RightArray.Length > rightIndex)
                {
                    //sort by width, our first coordinate
                    if (LeftArray[leftIndex].width > RightArray[rightIndex].width)
                    {
                        Boxes[k] = LeftArray[leftIndex];
                        leftIndex++;
                    }
                    else if(LeftArray[leftIndex].width < RightArray[rightIndex].width)
                    {
                        Boxes[k] = RightArray[rightIndex];
                        rightIndex++;
                    }
                    else
                    {
                        if (LeftArray[leftIndex].length > RightArray[rightIndex].length)
                        {
                            Boxes[k] = LeftArray[leftIndex];
                            leftIndex++;
                        }
                        else
                        {
                            Boxes[k] = RightArray[rightIndex];
                            rightIndex++;
                        }
                    }
                }
                //if right array has been transfarred but left has not-transferred values
                else if (LeftArray.Length > leftIndex)
                {
                    Boxes[k] = LeftArray[leftIndex];
                    leftIndex++;
                }
                //if left array has been transfarred but right has not-transferred values
                else
                {
                    Boxes[k] = RightArray[rightIndex];
                    rightIndex++;
                }
            }
        }
        public static void MergeSort(Box[] B, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                MergeSort(B, p, q);
                MergeSort(B, q + 1, r);
                Merge(B, p, q, r);
            }
        }
        static void Main(string[] args)
        {
            //to check whether an input file is converted or not
            bool fileProvided = false;

            //read whole input files with the format of input*.txt
            foreach (string inputFile in Directory.EnumerateFiles(folderPath, "input*.txt"))
            {
                //confirm that input file is provided
                fileProvided = true;

                //identify the file number so that input and output file numbers match
                int inputIndex = inputFile.LastIndexOf("input");
                int txtIndex = inputFile.IndexOf(".txt");
                string numberPart = inputFile.Substring(inputIndex+5, txtIndex  - inputIndex - 5);

                //create watch for measuring the execution time, we are creating watch in here because we want to see execution time for each
                //input and the above for loop is only for getting each input file hence it is not included in the main algotihm
                var watch = Stopwatch.StartNew();

                // variable definitions used in algorithm
                int N;
                Box[] Boxes;

                //get the execution time measurements into .txt file to be plotted later
                using (StreamWriter timeMeasurementFile = new StreamWriter(folderPath + "\\measurements.txt", append:true))
                {
                    //read the input file
                    using (StreamReader file = new StreamReader(inputFile))
                    {
                        string ln;
                        ln = file.ReadLine();

                        //if file is not empty or not end of file
                        if(ln != null)
                        {
                            N = Int32.Parse(ln);
                            Boxes = new Box[N];

                            //read boxes
                            for (int j = 0; j < N; j++)
                            {
                                //read dimensions
                                ln = file.ReadLine();

                                //split as W and L
                                string[] dimensions = ln.Split(',');

                                //getting max of dimensions as width, min of dimensions as length
                                int width = Math.Max(Int32.Parse(dimensions[0]), Int32.Parse(dimensions[1]));
                                int length = Math.Min(Int32.Parse(dimensions[1]), Int32.Parse(dimensions[0]));

                                //add both boxes into B
                                Boxes[j] = new Box(width, length);
                            }
                            
                            //sort the boxes according to their width
                            MergeSort(Boxes, 0, N - 1);
                           
                            //apply LDS accordig to length and get longest decreasing subsequence
                            LDS(Boxes, numberPart);
                           
                            file.Close();

                            //write to measurement.txt file the number of boxes and the execution time of the algorithm in time ticks
                            timeMeasurementFile.WriteLine(N + "," + watch.ElapsedTicks);

                            //stop the watch for another calculation
                            watch.Stop();
                            watch.Reset();
                        }
                    }
                }
            }
            //if input file is not provided generate a log.txt file as warning
            if (!fileProvided)
            {
                string outputfile = Directory.GetCurrentDirectory() + "\\log.txt";

                using (StreamWriter outputFile = new StreamWriter(outputfile))
                {
                    outputFile.WriteLine("Input file is not provided or empty");
                }
            }
        }
    }
}

