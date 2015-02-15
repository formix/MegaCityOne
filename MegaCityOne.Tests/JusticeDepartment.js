


function CanWithdrawFromAccount(principal, bankAccount) {
    return principal.IsInRole("BankUser") &&
        bankAccount.Owner == principal.Identity.Name;

}


function CanDepositToAccount(principal) {
    return principal.IsInRole("BankUser");
}


function CanDisplayBalance(principal, bankAccount) {
    return principal.IsInRole("BankCounselor") || 
        principal.IsInRole("BankUser") &&
        (bankAccount.Owner == principal.Identity.Name);
}
