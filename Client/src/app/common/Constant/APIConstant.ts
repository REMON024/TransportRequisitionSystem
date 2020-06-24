export module ApiConstant {
    export const AuthenticationApi = {
        Login: 'security/login',
        Logout: 'security/logout',
        ChangePassword: 'security/change-password',
        ForgotPassword: 'security/forgotpassword',
        ResetPassword: 'security/reset-password'
    };

    export const UserApi = {
        AddUser: 'Security/create-user',
        UpdateUser: 'Security/update-user',
        UserFilter: 'Security/get-users-by-filter',
        GetUser: 'Security/get-user',
        ResetPassword: 'Security/reset-password'
    };

    export const LogApi = {
        AuditLogFilter: 'Log/get-auditlog-by-filter',
        SessionLogFilter: 'Security/get-session-by-filter',
        CancelSession: 'Security/cancel-session'
    };

    export const AccessRight = {
        GetAllAccessRight: 'Security/get-all-access-control',
        GetAllAccessRightByRole: 'Security/get-access-control-by-role',
        SaveOrUpdateAccessRight: 'Security/save-update-access-control',
        GetAllRoleName:'Security/get-all-access-role-name'

    };

    export const RequisitionApi = {
        SaveRequisition: 'Requisition/SaveRequisition', 
        ApproveRequisition: 'Requisition/ApproveRequisition',
        RejectRequisition: 'Requisition/RejectRequisition',
        CancelRequisition: 'Requisition/CancelRequisition',

        GetRequisition: 'Requisition/GetFilteredRequisition',
        GetRequisitionByID: 'Requisition/GetRequisitionByID',
        CheckVehicle: 'Requisition/CheckVehicleAvailability',
        CheckDriver: 'Requisition/CheckDriverAvailability',
        DriverSchedule: 'Requisition/DriverSchedule',

        VehicleSchedule: 'Requisition/VehicleSchedule',
        GetRequisitionByEmployee: 'Requisition/GetRequisitionByEmployee'


    };

    export const VehicleTypeApi = {
        GetVehicle: 'Vehicle/get-vehicle-type'  ,
        saveVehicle:'Vehicle/save/docs',
        updateVehicle:'Vehicle/update',
        getVehicleByID:'Vehicle/GetVehicleByID',
        getAllVehicle:'Vehicle/GetAll',
        getVehicleByType:'Vehicle/GetVehicleByType'      
    };

    export const VehicleApi = {
        SaveVehicle: 'save-vehicle-document'        
    };
    export const EmployeeApi = {
        GetAllEmployee: 'Employee/getAllEmployee',
        GetAllEmployeeUsingVM:'Employee/getAllEmployeeUsingVM',
        GetEmployeeById:'Employee/getEmployeeById'

    };

    export const DriverApi = {
        SaveDriverInfo: 'Driver/SaveDriver'    ,
        GetDriverInfo: 'Driver/GetDriverInfo' ,
        GetDriverInfoByID: 'Driver/GetDriverInfoByID'     
    };

    export const TravelDetailsApi = {
        SaveTravelDetails: 'TravelDetails/SaveOrUpdateTravelDetails',
        GetTravelDetails: 'TravelDetails/GetTravelDetailsById',
        
              
    };

    export const AdvancedReportApi = {
        GetSearchResult: 'AdvancedReport/AdvancedReportSearch',
        
        
              
    };

    

    

}
