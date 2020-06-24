using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.Repository;
using NybSys.UnitOfWork;

namespace NybSys.TMS.BLL
{
    public class VehicleDocumentBLL : IVehicleDocumentBLL
    {
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<Vehicles> VehicleRepository
        {
            get
            {
                return _unitOfWork.Repository<Vehicles>();
            }
        }

        public VehicleDocumentBLL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<Vehicles> SaveVehicleDocument(VMVehicle vMVehicle)
        {
            Vehicles vehicles = vMVehicle.Vehicle;
            vehicles.VehicleDocuments = vMVehicle.lstVehicleDocument;

            await VehicleRepository.InsertAsync(vehicles);
            await _unitOfWork.SaveAsync();

            return vehicles;
        }
    }
}
