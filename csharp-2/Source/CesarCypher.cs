using System;
using System.Text.RegularExpressions;

namespace Codenation.Challenge
{
  public class CesarCypher : ICrypt, IDecrypt
  {
    public string Crypt(string message)
    => Digest(message, 3);
    public string Decrypt(string cryptedMessage)
    => Digest(cryptedMessage, -3);
    private string Digest(string message, int key)
    {
      var isOutOfRange = Regex.IsMatch(message, @"^[a-zA-Z0-9 ]*$");
      if (!isOutOfRange) 
        throw new ArgumentOutOfRangeException();
     
      var messageToLower = message == null ? throw new ArgumentNullException() : message.ToLower();
      
      var result = string.Empty;

      foreach (char ch in messageToLower)
      {
        if (!char.IsLetter(ch))
          result += ch;
        else
        {
          var value = (char)((((ch + key) - 97) % 26) + 97);
          char confirmValue = (char)(value < 97 ? (value + 26) : value);
          result += confirmValue;
        }
      }
      return result;
    }
  }
}
