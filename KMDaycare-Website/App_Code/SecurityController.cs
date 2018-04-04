using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

public class SecurityController : IPrincipal
{

    private IIdentity _identity;
    private string _role;

    public SecurityController(IIdentity identity, string role)
    {
        _identity = identity;
        _role = role;
    }

    public IIdentity Identity
    {
        get
        {
            return _identity;
        }
    }

    public bool IsInRole(string role)
    {
        if(_role == role)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}