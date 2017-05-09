using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class HomeViewBundle
    {
        public NewUserForm RegisterUser { get; set; }
        public LoginUserForm LoginUser { get; set; }
    }

    public class DashboardViewBundle
    {
        public List<Wedding> AllWeddings { get; set; }
        public User LogUser { get; set; }
        public ResponseForm NewResponse { get; set; }
    }

    public class ShowWeddingBundle
    {
        public Wedding TheWedding { get; set; }
        public string TheAddress { get; set; }
        public List<Response> Yeses { get; set; }
    }

}