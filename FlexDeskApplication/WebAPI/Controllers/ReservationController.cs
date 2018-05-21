using System.Collections.Generic;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;

namespace WebAPI.Controllers
{

    [Route("api/Reservation")]
    public class ReservationController : Controller
    {


        //private readonly IReservationProvider reservationProvider;
        //private readonly IReservationProcessor reservationProcessor;
        //private readonly IFlexDeskProvider flexdeskProvider;
        //private readonly IDepartmentProvider departmentProvider;
        //private readonly IFloorProvider floorProvider;
        //private readonly IBuildingProvider buildingProvider;
        //private readonly IUserProvider userProvider;
        //private readonly IReservationBll reservationBll;

        //public ReservationController(IReservationProvider reservationProvider, IReservationProcessor reservationProcessor, IFlexDeskProvider flexdeskProvider, IDepartmentProvider departmentProvider, IFloorProvider floorProvider, IBuildingProvider buildingProvider, IUserProvider userProvider, IReservationBll reservationBll)
        //{
        //    this.reservationProvider = reservationProvider;
        //    this.reservationProcessor = reservationProcessor;
        //    this.flexdeskProvider = flexdeskProvider;
        //    this.departmentProvider = departmentProvider;
        //    this.floorProvider = floorProvider;
        //    this.buildingProvider = buildingProvider;
        //    this.userProvider = userProvider;
        //    this.reservationBll = reservationBll;
        //}



        private readonly IFlexDeskProvider flexdeskProvider;
        private readonly IDepartmentProvider departmentProvider;
        private readonly IFloorProvider floorProvider;
        private readonly IBuildingProvider buildingProvider;
        private readonly IUserProvider userProvider;
        private readonly IReservationBll reservationBll;

        public ReservationController(IFlexDeskProvider flexdeskProvider, IDepartmentProvider departmentProvider, IFloorProvider floorProvider, IBuildingProvider buildingProvider, IUserProvider userProvider, IReservationBll reservationBll)
        {

            this.flexdeskProvider = flexdeskProvider;
            this.departmentProvider = departmentProvider;
            this.floorProvider = floorProvider;
            this.buildingProvider = buildingProvider;
            this.userProvider = userProvider;
            this.reservationBll = reservationBll;
        }


        // GET api/Reservation
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            //return reservationProvider.Get();
            return reservationBll.ShowAllReservations();
        }

        // GET api/Reservation/5
        [HttpGet("{id}", Name = "ReservationGet")]
        public Reservation Get(long id)
        {
            //var reservation = reservationProvider.GetById(id);
            var reservation = reservationBll.GetReservationById(id);
            reservation.FlexDesk = flexdeskProvider.GetById(reservation.FlexDeskId);
            reservation.FlexDesk.Department = departmentProvider.GetById(reservation.FlexDesk.DepartmentId);
            reservation.FlexDesk.Department.Floor = floorProvider.GetById(reservation.FlexDesk.Department.FloorId);
            reservation.FlexDesk.Department.Floor.Building = buildingProvider.GetById(reservation.FlexDesk.Department.Floor.BuildingId);
            reservation.User = userProvider.GetById(reservation.UserId);

            return reservation;

        }

        // POST api/Reservation
        [HttpPost]
        public void Post([FromBody]Reservation reservation)
        {
            reservation.FlexDesk = flexdeskProvider.GetById(reservation.FlexDeskId);
            reservation.User = userProvider.GetById(reservation.UserId);
            reservationBll.CreateReservation(reservation);
            //reservationProcessor.Create(reservation);
        }

        // PUT api/Reservation/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Reservation reservation)
        {
            reservation.ReservationId = id;
            reservationBll.UpdateReservation(id, reservation);
            //reservationProcessor.Update(reservation);
        }

        // DELETE api/Reservation/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            reservationBll.DeleteReservation(id);
            //reservationProcessor.Delete(id);
        }
    }
}
