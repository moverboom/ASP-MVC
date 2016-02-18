using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInviteOpdracht.Models {
    public class GuestResponseRepository : IRepository {
        bool result = false;
        private List<GuestResponse> responses;

        public GuestResponseRepository() {
            responses = new List<GuestResponse>();
        }

        public GuestResponse GetResponse(string email) {
            var foundResponses = responses.Select(g => g).Where(g => g.Email == email);
            if (foundResponses.Count() == 1) {
                return foundResponses.First();
            } else {
                throw new ArgumentOutOfRangeException("Response not found");
            }
        }

        public bool AddResponse(GuestResponse response) {
            //Use Linq to select the Guestresponse from the list where the Email addresses are equal
            var foundResponses = responses.Select(g => g).Where(g => g.Email == response.Email);

            //If no results are found, add the response and return true
            if (foundResponses.Count() == 1) {
                if (foundResponses.First().WillAttend == response.WillAttend) {
                    return false;
                } else {
                    responses.Remove(foundResponses.First());
                    //if (response.ID == null) response.ID = getNewID();
                    responses.Add(response);
                    return true;
                }
            } else {
                //if (response.ID == null) response.ID = getNewID();
                responses.Add(response);
                return true;
            }
        }

        public IEnumerable<GuestResponse> GetAllResponses() {
            return responses;
        }

        public bool EditResponse(GuestResponse response) {
            var foundResponse = responses.Select(r => r).Where(r => r.Email == response.Email);
            if (foundResponse.Count() == 1) {
                responses.Remove(foundResponse.First());
                responses.Add(response);
                return true;
            } else {
                return false;
            }
        }

        //public int getNewID() {
        //    var foundIDs = responses.OrderByDescending(r => r.ID).Select(r => r.ID);
        //    if (foundIDs.Count() > 0) {
        //        return foundIDs.First().Value + 1;
        //    } else {
        //        return 1;
        //    }
        //}
       
    }
}