using PlatformsService.Models;

namespace PlatformsService.Data
{
    public interface IPlatformsRepo
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform plat);
    }
}