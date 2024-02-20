using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Bankmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Bank: Bankmodel
    {
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
        public List<Banklist> Banklist { get; set; }
        public List<ledgergrouplist> ledgergrouplist { get; set; }
        public List<accountgrouplist> accountgrouplist { get; set; }
        public List<accountlist> accountlist { get; set; }
        public List<ledgerincomelist> ledgerincomelist { get; set; }
        public List<ledgerexpenselist> ledgerexpenselist { get; set; }
        public List<accountassetlist> accountassetlist { get; set; }
        public List<accountliabilitylist> accountliabilitylist { get; set; }
        public List<bankbooklist> bankbooklist { get; set; }
        public List<cashbooklist> cashbooklist { get; set; }
        public List<journallist> journallist { get; set; }
        public List<journaldtllist> journaldtllist { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string account_number { get; set; }
        public string ifsc_code { get; set; }
        public string swift_code { get; set; }
        public string neft_code { get; set; }
        public string bank_code { get; set; }
        public string account_type { get; set; }
        public string bank_accountgid { get; set; }
        public int company_gid { get; set; }
        public string company_name { get; set; }
        public string company_code { get; set; }
        public string company_address { get; set; }
        public string company_contact_number { get; set; }
        public string company_email_address { get; set; }
        public string contact_person { get; set; }
        public string account_gid { get; set; }
    }
    public class Banklist
    {
        public string bank_gid { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string account_type { get; set; }
        public string account_number { get; set; }
        public string ifsc_code { get; set; }
        public string neft_code { get; set; }
        public string swift_code { get; set; }
        public string accountgroup { get; set; }
        public string opening_balance { get; set; }
        public string branch_name { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string account_gid { get; set; }
        public string branch_gid { get; set; }
        public string account_name { get; set; }
        public double closing_amount { get; set; }
    }
    public class Bankdetails:Bankmodel
    {
        public string bank_gid { get; set; }
        public string bankcode { get; set; }
        public string bank_name { get; set; }
        public string account_type { get; set; }
        public string account_number { get; set; }
        public string ifsc_code { get; set; }
        public string neft_code { get; set; }
        public string swift_code { get; set; }
        public string accountgroup { get; set; }
        public string opening_balance { get; set; }
        public string branch_name { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string account_gid { get; set; }
        public string branch_gid { get; set; }
        public string account_name { get; set; }
        public string date { get; set; }   
        public string year { get; set; }   
    }
    public class journaldetails : Bankmodel
    {
        public string transaction_date { get; set; }
        public string remarks { get; set; }
        public string transaction_type { get; set; }
        public string reference_type { get; set; }
        public string reference_gid { get; set; }
        public string transaction_code { get; set; }
        public string journal_refnumber { get; set; }
        public string invoiceflag { get; set; }
        public string journal_from { get; set; }
        public string yearendactivityflag { get; set; }
        public string journalyear { get; set; }
        public string journalmonth { get; set; }
        public string journalday { get; set; }
        public string transaction_gid { get; set; }
        public string journal_type { get; set; }
        public double transaction_amount { get; set; }
        public string account_gid { get; set; }
        public List<journallist> journallist { get; set; }
        public List<accountlist> accountlist { get; set; }
        public string type { get; set; }
        public int journal_gid {get;set; }
        public string journaldtl_gid { get; set; }
        public string company_name { get; set; }
        public string bank_gid { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string account_type { get; set; }
        public string account_number { get; set; }
        public string ifsc_code { get; set; }
        public string neft_code { get; set; }
        public string swift_code { get; set; }
        public string account_group { get; set; }
        public string opening_balance { get; set; }
        public string branch_name { get; set; }  
        public string created_date { get; set; } 
        public string branch_gid { get; set; }
        public string account_name { get; set; }
        public string date { get; set; }
        public string year { get; set; }
        public string accountgroup_name { get; set; }
        public string ledger_type { get; set; }
        public string display_type { get; set; }
        public string gl_code { get; set; }
        public string account_code { get; set; }
        public string accountgroup_gid { get; set; }
        public string haschild { get; set; }
        public string ledger { get; set; }
    }
    public class chartofaccountdetails : Bankmodel
    {
        public string account_name { get; set; }
        public string accountgroup_name { get; set; }
        public string ledger_type { get; set; }
        public string display_type { get; set; }
        public string accountgroup_code { get; set; }
        public string gl_code { get; set; }
        public string account_code { get; set; }
        public string accountgroup_gid { get; set; }
        public string has_child { get; set; }
        public string ledger { get; set; }
        public List<accountgrouplist> accountgrouplist { get; set; }
    }
     public class ledgergrouplist 
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
    }
    public class accountgrouplist 
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
    }
    public class accountlist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
    }
    public class ledgerincomelist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
    }
    public class ledgerexpenselist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
    }
    public class accountassetlist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
    }
    public class accountliabilitylist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
    }
    public class journallist
    {
        
        public string remarks { get; set; }
        public string transaction_gid { get; set; }
        public string journal_type { get; set; }
        public string journal_type1 { get; set; }
        public double transaction_amount { get; set; }
        public string account_gid { get; set; }
        public int journal_gid { get; set; }
        public string transaction_type { get; set; }
        public string date { get; set; }
        public string journal_refnumber{ get; set; }
        public string journaldtl_gid { get; set; }
        public string account_name { get; set; }
        public string accountgroup_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string branch_name { get; set; }
    }
    public class bankbooklist
    {
        public int journal_gid { get; set; }
        public string journal_refnumber { get; set; }
        public string transaction_date { get; set; }
        public string bank_name { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string remarks { get; set; }
        public double credit_amount { get; set; }
        public double debit_amount { get; set; }
        public double closing_amount { get; set; }
        public string account_gid { get; set; }
        public string journaldtl_gid { get; set; }
        public string reference_gid { get; set; }
        public string account_desc { get; set; }
    }
    public class cashbooklist
    {
        public int journal_gid { get; set; }
        public string journal_refnumber { get; set; }
        public string transaction_date { get; set; }
        public string company_name { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string remarks { get; set; }
        public double credit_amount { get; set; }
        public double debit_amount { get; set; }
        public  double closing_amount { get; set; }
        public string account_gid { get; set; }
        public string journaldtl_gid { get; set; }
        public string reference_gid { get; set; }
        public string transaction_gid { get; set; }
        public string branch_name { get; set; }
        public string branch_gid { get; set; }
        public string account_desc { get; set; }
    }
    public class journaldtllist
    {
        public int journal_gid { get; set; }
        public string account_name { get; set; }
        public string voucher_type { get; set; }
        public string remarks { get; set; }
        public double credit_amount { get; set; }
        public double debit_amount { get; set; }
        public int journaldtl_gid { get; set; }
    }
}