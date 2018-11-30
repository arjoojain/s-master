



using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.IO;


namespace ConsoleApp1
{
    class ListJobsInResourceGroup
    {

        public static void DBConnection()
        {
            string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=xttestnorthcentralus;AccountKey=1oJNP/ABfLW2Aqd0s2o7NLrTEtAGtPUu33GEDuOsRH1h23Nylcsq5tOOmYoXB0qtSiHcPjZ94sWaLlll0fcDbw==;EndpointSuffix=core.windows.net";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object.

            string filter = "(PartitionKey eq 'e58c78fd-ae8b-45f1-bc2a-afdaa71e4c14' or PartitionKey eq 'eb2775af-00e5-48cf-ad7b-5b1848ab76e1' or PartitionKey eq 'ab5b0cd3-cd4d-4bcb-aa54-9c1a80fd49fc' or " +
                                            "PartitionKey eq '8865aa8b-1878-42cd-953b-da399e68fed9' or PartitionKey eq '7ea66689-dccc-4bd2-b47d-993e5c4914e6' or PartitionKey eq '6466bb17-128a-4e89-b4c9-681293f09953' " +
                                            " or PartitionKey eq '3991f177-0e7e-4adc-9ffd-0665a44c16d9') and " +
                                                          "RowKey gt 'j_' and RowKey lt 'j`'";

            string filter2 = "(PartitionKey eq 'e58c78fd-ae8b-45f1-bc2a-afdaa71e4c14') and " +
                                                         "RowKey gt 'j_' and RowKey lt 'j`'";


            CloudTable table = tableClient.GetTableReference("XTransport");

            var query = new TableQuery<JobEntity>().Where(filter2).Take(100);
            StreamWriter file =
            new StreamWriter(@"C:\Users\arja\source\repos\ConsoleApp1\ConsoleApp1\TextFile1.txt");

            //file.WriteLine(filter);

            file.WriteLine();

            Console.WriteLine("FilterCondition is : " + filter);

            var res = table.ExecuteQuery(query); ;
            

            double totalTime = 0;

            int count = 0;

            for (int i = 0; i < 1000; i++)
            {
                double startTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                res = table.ExecuteQuery(query);

               double endTime = 0;
               foreach (var job in res)
                {
                    endTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                    break;
                }

                count++;

                double currentExecutionTime = (endTime - startTime);

                totalTime = totalTime + (endTime - startTime);

                Console.WriteLine("Execution Time at Iteration "+count+" : " + (endTime - startTime));
                file.WriteLine("Execution Time at Iteration " + count + " : " + (endTime - startTime));
                file.WriteLine();
            }

            file.WriteLine();
            file.WriteLine("***************Average Time In Execution***********");
            file.WriteLine();
            file.WriteLine("Avg execution : " + totalTime / (1000*count)+" seconds");

            Console.WriteLine("Avg execution : " +totalTime/(1000*count));

            file.Close();
        }

    }
}
