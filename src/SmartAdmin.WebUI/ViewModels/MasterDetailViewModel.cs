using Cooperchip.ITDeveloper.Domain.Entities.MasterDetails;
using System.Collections.Generic;

namespace Cooperchip.ITDeveloper.Mvc.ViewModels
{
    public class MasterDetailViewModel
    {
        public List<Team> Teams { get; set; }
        public Team SelectedTeam { get; set; }
        public TeamMember SelectedTeamMember { get; set; }
        public DataEntryTargets DataEntryTarget { get; set; }
        public DataDisplayModes DataDisplayMode { get; set; }

    }
}
