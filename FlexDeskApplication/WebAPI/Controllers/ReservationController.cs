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

        //Ophalen BusinessLogica die doorgegeven wordt aan de WebApi Controller Klasse om een dependency te creëeren
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

        //Hieronder wordt de routering bepaalt door bepaalde action methods toe te wijzen. 
        //Deze zullen dan de requesten die binnenkomen behandelen en de juiste routering parameters meegeven (=attributeRouting)

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
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        {
            reservation.FlexDesk = flexdeskProvider.GetById(reservation.FlexDeskId);
            reservation.User = userProvider.GetById(reservation.UserId);
            reservationBll.CreateReservation(reservation);
        }

        // PUT api/Reservation/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Reservation reservation)
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        {
            reservation.ReservationId = id;
            reservationBll.UpdateReservation(id, reservation);
        }

        // DELETE api/Reservation/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            reservationBll.DeleteReservation(id);
        }
    }
}
