using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class RideRepo
    {
        Dictionary<string, List<Ride>> userRides = null;

        public RideRepo()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }
        public void AddRide(string userId, Ride[] rides)
        {
            bool rideList = this.userRides.ContainsKey(userId);
            try
            {
                if (!rideList)
                {
                    List<Ride> list = new List<Ride>();
                    list.AddRange(rides);
                    this.userRides.Add(userId, list);
                }
            }
            catch (CabException)
            {
                throw new CabException(CabException.ExceptionType.NULL_RIDES, "Rides are null");
            }
        }
        public Ride[] getRides(string userId)
        {
            bool rideList = this.userRides.ContainsKey(userId);
            try
            {
                return this.userRides[userId].ToArray();
            }
            catch (Exception)
            {
                throw new CabException(CabException.ExceptionType.INVALID_USER_ID, "Invalid user ID");
            }
        }
    }
}
