using System;
namespace WereldService.Models
{

    public class WorldRequest
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }

    public class WorldUpdateRequest : WorldRequest
    {
        public Guid WorldId { get; set; }
    }

    public class WorldDeleteRequest : WorldUpdateRequest
    {

    }

    public class WorldOverviewModel
    {
        public Guid WorldId { get; set; }
        public string Title { get; set; }
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
    }
}
