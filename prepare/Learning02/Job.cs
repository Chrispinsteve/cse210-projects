using System;

public class Job
{
  private string _jobTitle;
  private string _company;
  private string _duration;

  public Job(string jobTitle, string company, string duration)
  {
    _jobTitle = jobTitle;
    _company = company;
    _duration = duration;
  }

  public void Display()
  {
    Console.WriteLine($"{_jobTitle} ({_company}) {_duration}");
  }
}
