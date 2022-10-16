using PISWF.infrasrtucture.logger.context;
using PISWF.infrasrtucture.logger.model;
using PISWF.infrasrtucture.page;

namespace PISWF.infrasrtucture.logger.service;

public class LogService
{
    private LogRepository LogRepository { get; }

    public LogService(LogRepository logRepository)
    {
        LogRepository = logRepository;
    }

    public List<Log> Read(Page page)
    {
        return LogRepository.Entity.Skip(page.Size * page.Number).Take(page.Size).ToList();
    }
    
    public Log AddRecord(Log log)
    {
        LogRepository.Entity.Add(log);
        LogRepository.SaveChanges();
        return log;
    }
}