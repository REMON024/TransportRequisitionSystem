using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Employees
    {
        public Employees()
        {
            DriverInfos = new HashSet<DriverInfos>();
            Users = new HashSet<Users>();
        }

        public int CompanyId { get; set; }
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public long EmployeeId { get; set; }
        public int EmployeeType { get; set; }
        public int ShiftType { get; set; }
        public string Abbreviation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Lastname { get; set; }
        public string NationalId { get; set; }
        public string EmployeeNo { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public int NationalityId { get; set; }
        public int BankId { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountNo { get; set; }
        public string BranchCode { get; set; }
        public DateTime Dob { get; set; }
        public string PassportNo { get; set; }
        public int OccupassionalCategoryId { get; set; }
        public int ObjectType { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string BiometricId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        public byte[] FingurePrint { get; set; }
        public byte[] FingurePrintImage { get; set; }
        public byte[] FingurePrintRightIndex { get; set; }
        public byte[] FingurePrintImageRightIndex { get; set; }
        public byte[] FingurePrintRightMiddle { get; set; }
        public byte[] FingurePrintImageRightMiddle { get; set; }
        public byte[] FingurePrintLeftThumb { get; set; }
        public byte[] FingurePrintImageLeftThumb { get; set; }
        public byte[] FingurePrintLeftIndex { get; set; }
        public byte[] FingurePrintImageLeftIndex { get; set; }
        public byte[] FingurePrintLeftMiddle { get; set; }
        public byte[] FingurePrintImageLeftMiddle { get; set; }
        public string LicenseId { get; set; }
        public int Grade { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime EnrolledDate { get; set; }
        public int EnrolledBy { get; set; }
        public DateTime EnrolledUpdatedDate { get; set; }
        public int EnrolledUpdatedBy { get; set; }
        public int Status { get; set; }

        public Departments Department { get; set; }
        public EmployeeTypes EmployeeTypeNavigation { get; set; }
        public Units Unit { get; set; }
        public ICollection<DriverInfos> DriverInfos { get; set; }
        public ICollection<Users> Users { get; set; }
        public Designation Designation { get; set; }

        public ICollection<Requisitions> requisitions { get; set; }
        

    }
}
