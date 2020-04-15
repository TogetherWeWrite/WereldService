using System;
namespace WereldService.Models
{

    public class WorldRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }

    public class WorldUpdateRequest : WorldRequest
    {
        public Guid WorldId { get; set; }
    }

    public class WorldDeleteRequest : WorldUpdateRequest
    {

    }
}
