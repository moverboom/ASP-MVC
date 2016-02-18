using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInviteOpdracht.Models {
    public interface IRepository {
        GuestResponse GetResponse(string email);

        IEnumerable<GuestResponse> GetAllResponses();

        bool AddResponse(GuestResponse response);

        bool EditResponse(GuestResponse response);
    }
}