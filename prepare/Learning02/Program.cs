using System;

class Program
{
    static void Main()
    {
        //Create job instances
        Job job1 = new Job("Software Engineer","Microsoft","2019-2022");
        Job job2 = new Job("Manager", "Apple", "2022-2023");

        //Display job details using Display method
        job1.Display();
        job2?Display();

        //Create a resume instance
        Resume myResume = new Resume("John Doe");

        //Add jobs to the instance
        myResume.AddJob(job1);
        myResume.AddJob(job2);

        myResume.Display();
    }
}
