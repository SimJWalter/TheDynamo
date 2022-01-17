using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace LatencyTesting
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            int loopFor = 0;
            if (args.Length > 0)
            {
                loopFor = Int32.Parse(args[0]);
            }

            //create delay configuration object
            Hashtable properties = new Hashtable();
            properties.Add("tapQuantity", "5");
            properties.Add("delayLength", "500");

            EffectConfiguration config = new EffectConfiguration(44000, 128, properties);

            //pseudo-buffer
            float[] buffer = new float[128];

            try
            {
                // build (nested) delay effect(s)
                DummyDelayV3 v2_1 = new DummyDelayV3();
                v2_1.Setup(config);

                for (int i = 0; i < loopFor; i++)
                {
                    DummyDelayV3 temp = new DummyDelayV3();
                    temp.Setup(config);
                    v2_1.Decorator = temp;
                }

                // storage for the average time instance for calculation
                double avgTime = -1;
                double avgCalReqTime = -1;

                bool first = true;
                //start testing
                while (true)
                {
                    //calculate the time taken to request the current time in MS from the Calendar

                    DateTime startTime = DateTime.Now;
                    DateTime endTime = DateTime.Now;
                    TimeSpan duration = endTime - startTime;
                    if (avgCalReqTime >= 0)
                    {
                        avgCalReqTime += duration.TotalMilliseconds;
                        avgCalReqTime /= 2;
                    }
                    else
                    {
                        avgCalReqTime = duration.TotalMilliseconds;
                    }
                    //send reading to output
                    Console.WriteLine("MS for time from Calendar: " + (float)avgCalReqTime);

                    //load the buffer with fresh data
                    double[] samples = SampleData.Data;

                    for (int i = 0; i < samples.Length; i++)
                    {
                        buffer[i] = (float)samples[i];
                    }

                    float[] test = new float[128];
                    Array.Copy(buffer, test, buffer.Length);

                    //record start time position
                    startTime = DateTime.Now;
                    //run effects processor
                    for (int i = 0; i < 350; i++)
                    {
                        v2_1.Process(buffer);
                    }
                    //record end time position
                    endTime = DateTime.Now;
                    duration = endTime - startTime;

                    //calculate time as an average over all processors run during while-loop construct
                    if (avgTime >= 0)
                    {
                        avgTime += duration.TotalMilliseconds;
                        avgTime /= 2;
                    }
                    else
                    {
                        avgTime = duration.TotalMilliseconds;
                    }
                    //output the time taken for the effects processor
                    Console.WriteLine("Average time to process: " + avgTime);
                    double tmp = avgTime;
                    tmp -= avgCalReqTime;
                    Console.WriteLine("Average process time - average calendar request: " + tmp);

                    //run a check to make sure that the transformation has effected the buffer sent in as a parameter
                    bool equal = false;
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        equal = buffer[i] == test[i];
                    }
                    Console.WriteLine("Contains equal: " + equal + "\n");

                    if (first)
                    {
                        first = false;
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            Console.WriteLine(buffer[i]);
                        }
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                handleMe(e);
            }
        }

        public static void handleMe(Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.ReadLine();
        }
    }
}
