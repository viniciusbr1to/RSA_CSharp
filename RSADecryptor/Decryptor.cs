using System;
using System.Text;

class RSADecryptor{
  static long ModPow(long base_number, long exp, long mod){
      long result = 1;
    
    base_number %= mod;
    while(exp > 0){
      if(exp % 2 == 1)
         result = result * base_number % mod;
      base_number = base_number * base_number % mod;
      exp /= 2;
      }
      return result;
    }


  static string Decrypt(long[] cipher, long d, long n){
    byte[] bytes = new byte[cipher.Length];
    for(int i = 0; i < cipher.Length; i++)
      bytes[i] = (byte)ModPow(cipher[i], d, n);
    return Encoding.UTF8.GetString(bytes);
  }


  static void Main(){
      long n, d;
    
    Console.WriteLine("Insira a chave privada (n): ");
    n = long.Parse(Console.ReadLine());

    Console.WriteLine("Insira a chave privada (d): ");
    d = long.Parse(Console.ReadLine());

    Console.WriteLine("Insira o texto cifrado: ");
    string[] parts = Console.ReadLine().Split(' ');

    long[] cipher = new long[parts.Length];
    for(int i = 0; i < parts.Length; i++)
        cipher[i] = long.Parse(parts[i]);

    string decrypted = Decrypt(cipher, d, n);
    Console.WriteLine("\nTexto decifrado: ");
    Console.WriteLine(decrypted);
  }
}
