using System;
using System.Text;

class RSAEncryptor{
   static long GCD(long a, long b){
      while(b != 0) (a, b) = (b, a % b);
      return a;
   }


   static bool prime_number(long number){
      if(number < 2) return false;
      if(number == 2) return true;
      if(number % 2 == 0) return false;

      for(long i = 3; i * i <= number; i += 2)
          if(number % i == 0) return false;

      return true;
   }


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


   static long[] Encrypt(string text, long e, long n){
         byte[] bytes = Encoding.UTF8.GetBytes(text);
         long[] cipher = new long[bytes.Length];

      for(int i = 0; i < bytes.Length; i++)
          cipher[i] = ModPow(bytes[i], e, n);
      return cipher;
   }


   static string Decrypt(long[] cipher, long d, long n){
         byte[] bytes = new byte[cipher.Length];

      for(int i = 0; i < cipher.Length; i++)
          bytes[i] = (byte)ModPow(cipher[i], d, n);
      return Encoding.UTF8.GetString(bytes);
   }


   static void Main(){
         long p, q, n, e, d;
         long euler;

      Console.WriteLine("Insira o valor de (p): ");
      p = long.Parse(Console.ReadLine());
      if(!prime_number(p)){
         Console.WriteLine("O valor (p) precisa ser um numero primo.");
         return;
      }

      Console.WriteLine("Insira o valor de (q): ");
      q = long.Parse(Console.ReadLine());
      if(!prime_number(q)){
         Console.WriteLine("O valor (q) precisa ser um numero primo.");
         return;
      }

      n = p * q;
      euler = (p - 1) * (q - 1);

      Console.WriteLine("Insira o valor de (e): ");
      e = long.Parse(Console.ReadLine());
      if(GCD(e, euler) != 1){
         Console.WriteLine("mdc(e, phi) deve ser 1.");
         return;
      }

      long i = 1;
      d = 0;
      while(true){
         if((i * e) % euler == 1) {
            d = i;
            break;
         }
         i++;
      }

      if(n < 256){
         Console.WriteLine("p * q deve ser maior que 255. Escolha primos maiores.");
         return;
      }

      Console.WriteLine($"Chave publica: (n = {n}, e = {e})");
      Console.WriteLine($"Chave privada: (n = {n}, d = {d})");

      Console.WriteLine("\nInsira o texto para cifrar: ");
      string text = Console.ReadLine();

      long[] cipher = Encrypt(text, e, n);
      Console.WriteLine("\nTexto cifrado:");
      Console.WriteLine(string.Join(" ", cipher));

      string decrypted  = Decrypt(cipher, d, n);
      Console.WriteLine("\nTexto decifrado: ");
      Console.WriteLine(decrypted );
    }
}