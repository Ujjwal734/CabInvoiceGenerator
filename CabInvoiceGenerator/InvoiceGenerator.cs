using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        RideTypes rideType;
        private RideRepo rideRepository;
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;

        public InvoiceGenerator(RideTypes rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepo();
            try
            {
                if (rideType.Equals(RideTypes.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (rideType.Equals(RideTypes.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }

            }
            catch (CabException)
            {
                throw new CabException(CabException.ExceptionType.INVALID_RIDE_TYPE, "Invalid ride type");
            }
        }

        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabException(CabException.ExceptionType.INVALID_RIDE_TYPE, "Invalid ride type");
                }
                if (distance <= 0)
                {
                    throw new CabException(CabException.ExceptionType.INVALID_DISTANCE, "Invalid distance");
                }
                if (time < 0)
                {
                    throw new CabException(CabException.ExceptionType.INVALID_TIME, "Invalid time");

                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }


        public InvoiceCalculate CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);

                }
            }
            catch (CabException)
            {
                if (rides == null)
                {
                    throw new CabException(CabException.ExceptionType.NULL_RIDES, "rides are null");
                }

            }
            return new InvoiceCalculate(rides.Length, totalFare);
        }

        public InvoiceCalculate GetInvoiceSummary(String userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.getRides(userId));
            }
            catch (CabException)
            {
                throw new CabException(CabException.ExceptionType.INVALID_USER_ID, "Invalid user id");
            }
        }
    }
}
