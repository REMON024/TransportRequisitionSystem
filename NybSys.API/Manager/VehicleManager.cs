using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NybSys.API.Helper;
using NybSys.AuditLog.BLL;
using NybSys.Generic.BLL;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public class VehicleManager : IVehicleTypeManager
    {
        private readonly IGenericBLL _genericBLL;
        private readonly IAuditLogBLL _auditLogBLL;
        public VehicleManager(IGenericBLL genericBLL,IAuditLogBLL auditLogBLL)
        {
            _genericBLL = genericBLL;
            _auditLogBLL = auditLogBLL;
        }

        public virtual async Task<IActionResult> GetVehicleType(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetVehicleType), "Get vehicle type", Common.Enums.Action.View, message);
                List<VehicleTypes> lstVehicleType = await _genericBLL.GetByFilterAsync<VehicleTypes>(v => true);
                return Build.SuccessMessage(lstVehicleType);

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetVehicleType", 0, "VehicleManager");
                return Build.ExceptionMessage(ex);
            }
        }
        public virtual async Task<IActionResult> GetAllVehicle(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetAllVehicle), "Get all vehicle", Common.Enums.Action.View, new ApiCommonMessage());
                List<Vehicles> lstVehicle = await _genericBLL.GetByFilterAsync<Vehicles>(v => true);
                return Build.SuccessMessage(lstVehicle);

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetAllVehicle", 0, "VehicleManager");
                return Build.ExceptionMessage(ex);
            }
        }
        public virtual async Task<IActionResult> GetVehicleByID(ApiCommonMessage msg)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetVehicleByID), "Get vehicle by id", Common.Enums.Action.View, msg);
                VMVehicle vMVehicle = new VMVehicle();
                Vehicles vehicles = msg.GetRequestObject<Vehicles>();
                List<Vehicles> lstVehicle = await _genericBLL.GetByFilterAsync<Vehicles>(v => v.VehicleID==vehicles.VehicleID);
                if (lstVehicle.Count>0)
                {
                    vMVehicle.Vehicle = lstVehicle[0];
                    vMVehicle.lstVehicleDocument = await _genericBLL.GetByFilterAsync<VehicleDocuments>(v=>v.VehicleId== lstVehicle[0].VehicleID);
                }
                
                return Build.SuccessMessage(vMVehicle);

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(msg, ex.ToString(), ex.Message, msg, "GetVehicleByID", 0, "VehicleManager");
                return Build.ExceptionMessage(ex);
            }
        }
        public virtual async Task<IActionResult> Save(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(Save), "Save vehicle type", Common.Enums.Action.Insert, message);
                VehicleTypes vehicleTypes = message.GetRequestObject<VehicleTypes>();
                VehicleTypes result = await _genericBLL.InsertAsync<VehicleTypes>(vehicleTypes);
                await _genericBLL.SaveChangesAsync();

                return Build.SuccessMessage(result);

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "Save", 0, "VehicleManager");
                return Build.ExceptionMessage(ex);
            }
        }
        public virtual async Task<IActionResult> UpdateVehicle(ApiCommonMessage msg)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(UpdateVehicle), "Update vehicle", Common.Enums.Action.Update, msg);
                VMVehicle vmVehicle = JsonConvert.DeserializeObject<VMVehicle>(msg.Content.ToString());
                _genericBLL.Update(vmVehicle.Vehicle);
                _genericBLL.SaveChanges();
                List<VehicleDocuments> lst =await _genericBLL.GetByFilterAsync<VehicleDocuments>(v=>v.VehicleId==vmVehicle.Vehicle.VehicleID);
                foreach (VehicleDocuments item in lst)
                {
                    _genericBLL.Delete(item);
                }
                _genericBLL.SaveChanges();
                foreach (VehicleDocuments item in vmVehicle.lstVehicleDocument)
                {
                    item.VehicleDocumentId = 0;
                    item.VehicleId = vmVehicle.Vehicle.VehicleID;
                    await _genericBLL.InsertAsync<VehicleDocuments>(item);
                   
                }
                _genericBLL.SaveChanges();
                return Build.SuccessMessage(vmVehicle);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(msg, ex.ToString(), ex.Message, msg, "UpdateVehicle", 0, "VehicleManager");
                return Build.ExceptionMessage(ex);
            }
        }
        public virtual async Task<IActionResult> InsertVehicleDetails(ApiCommonMessage msg)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(InsertVehicleDetails), "Insert vehicle details", Common.Enums.Action.Insert, msg);
                VMVehicle vmVehicle = JsonConvert.DeserializeObject<VMVehicle>(msg.Content.ToString());
                await _genericBLL.InsertAsync<Vehicles>(vmVehicle.Vehicle);
                await _genericBLL.SaveChangesAsync();
                foreach (VehicleDocuments item in vmVehicle.lstVehicleDocument)
                {
                    item.VehicleId = vmVehicle.Vehicle.VehicleID;
                    await _genericBLL.InsertAsync<VehicleDocuments>(item);
                    await _genericBLL.SaveChangesAsync();
                }

                return Build.SuccessMessage(vmVehicle);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(msg, ex.ToString(), ex.Message, msg, "InsertVehicleDetails", 0, "VehicleManager");
                return Build.ExceptionMessage(ex);
            }

          
        }

        public virtual async Task<IActionResult> GetVehicleByType(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetAllVehicle), "Get all vehicle", Common.Enums.Action.View, new ApiCommonMessage());

                int vehicleTypes = Convert.ToInt32(message.GetRequestObject<string>());
                List<Vehicles> lstVehicle = await _genericBLL.GetByFilterAsync< Vehicles>( v => v.VehicleTypeID== vehicleTypes);
                return Build.SuccessMessage(lstVehicle);

            }
            catch (Exception ex)
            {
                return Build.ExceptionMessage(ex);
            }
        }
    }
}
