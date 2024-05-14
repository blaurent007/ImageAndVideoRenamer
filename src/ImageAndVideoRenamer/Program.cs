using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace ImageAndVideoRenamer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //String SourcePath = @"C:\Users\BLAU\Desktop\USA2024\iCloud Photos\iCloud Photos";
            //String DestinationFilePath = @"C:\Users\BLAU\Desktop\USA2024\iCloud Photos\Renamed";

            String SourcePath = @"C:\DEV\202405__";
            String DestinationFilePath = @"C:\DEV\TEST";
            String NewFilePrefix = "USA2024";

            


            var filelist = System.IO.Directory.GetFiles(SourcePath).Where(o => o.ToLower().EndsWith(".jpg") || o.ToLower().EndsWith(".jpeg") || o.ToLower().EndsWith(".png") || o.ToLower().EndsWith(".mp4") || o.ToLower().EndsWith(".mov"));
            
            foreach (var pix in filelist)
            {

                Console.WriteLine(pix);
                String imagePath = pix;
                DateTime TakenDateTime = DateTime.Now;

                IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(imagePath);

                if (imagePath.ToLower().EndsWith(".jpg") || imagePath.ToLower().EndsWith(".jpeg"))
                {
                    //7 - Date/Time = 2024:05:04 08:49:05
                    try
                    {

                        String dateTime = directories.ToArray()[1].Tags[7].Description;

                        var lst = dateTime.Split(':');

                        int year = int.Parse(lst[0]);
                        int month = int.Parse(lst[1]);
                        int day = int.Parse(lst[2].Split(' ')[0]);
                        int hour = int.Parse(lst[2].Split(' ')[1]);
                        int min = int.Parse(lst[3]);
                        int sec = int.Parse(lst[4]);
                        TakenDateTime = new DateTime(year, month, day, hour, min, sec);

                        Console.WriteLine(dateTime);
                        Console.WriteLine(TakenDateTime);
                    }
                    catch (Exception ex)
                    {
                        try
                        {

                            String dateTime = directories.ToArray()[2].Tags[7].Description;

                            var lst = dateTime.Split(':');

                            int year = int.Parse(lst[0]);
                            int day = int.Parse(lst[2].Split(' ')[0]);
                            int month = int.Parse(lst[1]);
                            int hour = int.Parse(lst[2].Split(' ')[1]);
                            int min = int.Parse(lst[3]);
                            int sec = int.Parse(lst[4]);
                            TakenDateTime = new DateTime(year, month, day, hour, min, sec);

                            Console.WriteLine(dateTime);
                            Console.WriteLine(TakenDateTime);

                        }
                        catch (Exception)
                        {
                            try
                            {

                                String dateTime = directories.ToArray().Last().Tags[2].Description;

                                var lst = dateTime.Split(' ');

                                int year = int.Parse(lst.Last());
                                int day = int.Parse(lst[2]);
                                int month = Convert.ToDateTime(lst[1] + " 01, 1900").Month; // month
                                int hour = int.Parse(lst[3].Split(':')[0]);
                                int min = int.Parse(lst[3].Split(':')[1]);
                                int sec = int.Parse(lst[3].Split(':')[2]);
                                TakenDateTime = new DateTime(year, month, day, hour, min, sec);

                                Console.WriteLine(dateTime);
                                Console.WriteLine(TakenDateTime);

                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }
                    }
                    
                }

                //if (imagePath.ToLower().EndsWith(".jpeg") )
                //{
                //    //7 - Date/Time = 2024:05:04 08:49:05
                //    String dateTime = directories.ToArray()[2].Tags[7].Description;

                //    var lst = dateTime.Split(':');

                //    int year = int.Parse(lst[0]);
                //    int day = int.Parse(lst[1]);
                //    int month = int.Parse(lst[2].Split(' ')[0]);
                //    int hour = int.Parse(lst[2].Split(' ')[1]);
                //    int min = int.Parse(lst[3]);
                //    int sec = int.Parse(lst[4]);
                //    TakenDateTime = new DateTime(year, month, day, hour, min, sec);

                //    Console.WriteLine(dateTime);
                //    Console.WriteLine(TakenDateTime);
                //}

                if (imagePath.ToLower().EndsWith(".png"))
                {
                    //7 - Date/Time = 2024:05:04 08:49:05
                    try
                    {

                        String dateTime = directories.ToArray()[3].Tags[0].Description;

                        var lst = dateTime.Split(':');

                        int year = int.Parse(lst[0]);
                        int day = int.Parse(lst[2].Split(' ')[0]);
                        int month = int.Parse(lst[1]);
                        int hour = int.Parse(lst[2].Split(' ')[1]);
                        int min = int.Parse(lst[3]);
                        int sec = int.Parse(lst[4]);
                        TakenDateTime = new DateTime(year, month, day, hour, min, sec);

                        Console.WriteLine(dateTime);
                        Console.WriteLine(TakenDateTime);

                    }
                    catch (Exception)
                    {

                        try
                        {

                       
                            String dateTime = directories.ToArray()[4].Tags[0].Description;

                            var lst = dateTime.Split(':');

                            int year = int.Parse(lst[0]);
                            int day = int.Parse(lst[2].Split(' ')[0]);
                            int month = int.Parse(lst[1]);
                            int hour = int.Parse(lst[2].Split(' ')[1]);
                            int min = int.Parse(lst[3]);
                            int sec = int.Parse(lst[4]);
                            TakenDateTime = new DateTime(year, month, day, hour, min, sec);

                            Console.WriteLine(dateTime);
                            Console.WriteLine(TakenDateTime);

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                }

                if (imagePath.ToLower().EndsWith(".mp4") || imagePath.ToLower().EndsWith(".mov"))
                {
                    //2 - Created = Sat May 11 04:43:22 2024
                    String dateTime = directories.ToArray()[1].Tags[2].Description;

                    var lst = dateTime.Split(' ');

                    int year = int.Parse(lst.Last());
                    int day = int.Parse(lst[2]);
                    int month = Convert.ToDateTime(lst[1] + " 01, 1900").Month; // month
                    int hour = int.Parse(lst[3].Split(':')[0]);
                    int min = int.Parse(lst[3].Split(':')[1]);
                    int sec = int.Parse(lst[3].Split(':')[2]);
                    TakenDateTime = new DateTime(year, month, day, hour, min, sec);

                    Console.WriteLine(dateTime);
                    Console.WriteLine(TakenDateTime);
                }

                String newFileName = string.Empty;
                int i = 0;

                newFileName =  NewFilePrefix + "_" + TakenDateTime.ToString("yyyyMMdd_HHmmss") + "_" + i.ToString() + System.IO.Path.GetExtension(pix);
                newFileName = newFileName.ToLower();

                while (System.IO.File.Exists(System.IO.Path.Combine(DestinationFilePath, newFileName)))
                {
                    i++;
                    newFileName = NewFilePrefix + "_" + TakenDateTime.ToString("yyyyMMdd_HHmmss") + "_" + i.ToString() + System.IO.Path.GetExtension(pix);
                    newFileName = newFileName.ToLower();
                }

                System.IO.File.Copy(pix, System.IO.Path.Combine(DestinationFilePath, newFileName));

            }

            //foreach (MetadataExtractor.Directory d in directories)
            //{
            //    Console.WriteLine(d.Name);
            //    int i = 0;
            //    foreach (var tag in d.Tags)
            //        Console.WriteLine($"{i++} - {tag.Name} = {tag.Description}");
            //}

            Console.Read();
        }

       

    }
}
