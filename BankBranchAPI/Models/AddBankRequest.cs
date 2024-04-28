using System.ComponentModel.DataAnnotations;

namespace BankBranchAPI.Models
{
    public class AddBankRequest 
    {
        [Required]
        public string name {  get; set; }

        [Url]
        public string location { get; set; }
        public string branchManager { get; set; }






    }

    public class BankBranchResponse // to not confuse the customer 
    {
        public string name { get; set; }

        public string location { get; set; }

        public string branchManager { get; set; }
    }
}
