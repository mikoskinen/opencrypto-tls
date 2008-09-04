﻿//#define ECC
//#define DSS
#define RSA
#if !(ECC || DSS || RSA)
#error 利用する証明書のタイプを選択してください
#endif
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using openCrypto.EllipticCurve.Signature;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace openCrypto.TLS
{
	public class AppLoader
	{
		static void Main ()
		{
			AsymmetricAlgorithm signAlgo;
#if ECC
			X509Certificate cert = new X509Certificate ("localhost.x509");
			ECDSA ecdsa = new ECDSA (openCrypto.EllipticCurve.ECDomainNames.secp256r1);
			ecdsa.Parameters.PrivateKey = new byte[] {0x31, 0xfe, 0xa8, 0xf8, 0xdb, 0x32, 0x57, 0x79, 0xb2, 0xaf, 0xb6, 0x34, 0xef, 0xe6, 0x60,
				0x00, 0x75, 0xa5, 0xd3, 0xa6, 0xba, 0x7a, 0x07, 0xc1, 0x5b, 0x8f, 0x81, 0xe1, 0xce, 0x48,
				0xb2, 0x9a};
			signAlgo = ecdsa;
#elif DSS
			X509Certificate cert = new X509Certificate ("localhost.dss.x509");
			DSACryptoServiceProvider dsa = new DSACryptoServiceProvider ();
			DSAParameters dsaParam = new DSAParameters ();
			dsaParam.G = new byte[] {0x86, 0x41, 0x6a, 0x6f, 0xec, 0xdd, 0xaf, 0x20, 0x23, 0xd6, 0x01, 0x10, 0xb7, 0xd0, 0xc7, 0x1d, 0xfe, 0xfc, 0x16, 0x9d, 0xba, 0x82, 0xf2, 0xb0, 0x9e, 0x7e, 0x40, 0x0e, 0xb5, 0x0c, 0x04, 0x01, 0xdf, 0xe1, 0xc3, 0x3a, 0x45, 0xe2, 0xf1, 0x47, 0x4f, 0xd1, 0x35, 0x5b, 0x2e, 0x59, 0x91, 0xdb, 0x1d, 0xeb, 0xa0, 0xa8, 0x7c, 0xd3, 0x56, 0x32, 0xd7, 0xd4, 0x52, 0x86, 0xfc, 0xc5, 0xba, 0x60, 0xbe, 0x70, 0x45, 0x23, 0x8b, 0xdc, 0x27, 0x3c, 0x06, 0xb1, 0x23, 0xf1, 0x7f, 0xc2, 0x2a, 0x15, 0xb6, 0x2f, 0xbd, 0x9e, 0x0b, 0x6f, 0x57, 0xa3, 0xb3, 0x31, 0x0e, 0xd2, 0xd5, 0xdc, 0xf8, 0x6f, 0x51, 0xd7, 0x3d, 0x03, 0x9a, 0x1e, 0xf8, 0xf0, 0xbd, 0x57, 0x36, 0xe4, 0x95, 0xcf, 0x09, 0xdb, 0x49, 0x7e, 0x96, 0x45, 0x12, 0x6d, 0xfd, 0xff, 0xba, 0x2f, 0xd6, 0x55, 0xc3, 0x76, 0x2b, 0x9a};
			dsaParam.P = new byte[] {0xc7, 0x8c, 0x4d, 0x21, 0x12, 0x1f, 0x84, 0x3d, 0x43, 0xc1, 0xd7, 0xba, 0xf9, 0xd8, 0x97, 0x56, 0x7a, 0xc4, 0xed, 0x3a, 0xff, 0x53, 0x9d, 0x2c, 0x97, 0xf9, 0x25, 0x38, 0xb6, 0xdc, 0x54, 0xb4, 0x86, 0xd1, 0x55, 0xf9, 0x43, 0xd1, 0xd1, 0x1a, 0x21, 0x7e, 0x89, 0x49, 0xea, 0xa2, 0x7b, 0x15, 0x46, 0x60, 0x1b, 0xa4, 0x69, 0x9f, 0xe1, 0x63, 0x27, 0xc6, 0x00, 0x90, 0x7a, 0x9e, 0x7e, 0x20, 0x95, 0xfe, 0xa0, 0x08, 0xef, 0xc5, 0x73, 0x38, 0x8d, 0xc9, 0x04, 0xb7, 0x8d, 0xe6, 0x44, 0xd7, 0x2b, 0x37, 0x74, 0x81, 0x84, 0x62, 0x09, 0x84, 0xa9, 0xf2, 0x60, 0x32, 0x02, 0xa5, 0xc7, 0x90, 0xce, 0xdc, 0x32, 0x94, 0x15, 0x6b, 0x7e, 0x90, 0xc8, 0x14, 0xb4, 0x06, 0x6f, 0x44, 0xee, 0x35, 0xe0, 0x40, 0xcd, 0xe0, 0x76, 0x48, 0x79, 0x9d, 0x72, 0xf6, 0xc8, 0xef, 0x86, 0x5e, 0x45, 0xcf};
			dsaParam.Q = new byte[] {0x91, 0x66, 0x17, 0xd0, 0xb0, 0xb7, 0xfd, 0xff, 0xef, 0xfd, 0x31, 0x9b, 0x37, 0xd2, 0x5a, 0x2a, 0xbb, 0x99, 0xa0, 0x79};
			dsaParam.X = new byte[] {0x0a, 0x2e, 0xc0, 0x41, 0x1f, 0xdc, 0x08, 0x64, 0x24, 0xd2, 0xde, 0x64, 0x05, 0x56, 0xe6, 0x63, 0xfd, 0x52, 0x56, 0x99};
			dsa.ImportParameters (dsaParam);
			signAlgo = dsa;
#elif RSA
			X509Certificate cert = new X509Certificate ("localhost.rsa.x509");
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider ();
			RSAParameters rsaParams = new RSAParameters ();
			rsaParams.Exponent = new byte[] {1, 0, 1};
			rsaParams.Modulus = new byte[] {0xdb, 0x48, 0x27, 0xb7, 0x61, 0xa0, 0xb9, 0x28, 0x60, 0xb3, 0x7e, 0x3e, 0x59, 0x9f, 0x02, 0x9c, 0x9f, 0x4a, 0xa6, 0x1e, 0xba, 0xc7, 0x97, 0x55, 0xb7, 0x9c, 0x68, 0xaf, 0x37, 0x23, 0xc0, 0x78, 0x9d, 0xab, 0x2a, 0x63, 0xaf, 0x44, 0xb6, 0x1f, 0x30, 0x4c, 0x3b, 0x03, 0x85, 0x44, 0x20, 0xc5, 0xfc, 0x00, 0x76, 0x82, 0x48, 0x85, 0xc8, 0x53, 0x24, 0x55, 0x29, 0xd5, 0xce, 0x13, 0xd1, 0xb6, 0x89, 0x63, 0x9b, 0x42, 0x63, 0x26, 0x76, 0x47, 0xed, 0x95, 0xe2, 0x76, 0x59, 0xf2, 0x99, 0xdf, 0xb2, 0xb7, 0x86, 0xf5, 0x02, 0xbb, 0x81, 0x8f, 0xa0, 0x20, 0xd3, 0x8f, 0x1d, 0x92, 0x4e, 0x76, 0x7b, 0x8a, 0x31, 0xc7, 0x66, 0xc3, 0xff, 0x9f, 0xa5, 0x9d, 0xfd, 0xc2, 0x93, 0x55, 0x37, 0x7b, 0x68, 0x30, 0xca, 0xb7, 0x39, 0xd6, 0x21, 0x00, 0x69, 0x79, 0x2d, 0x9f, 0x24, 0x08, 0x5c, 0xb7};
			rsaParams.D = new byte[] {0x5b, 0x84, 0x89, 0xce, 0xe7, 0x58, 0x04, 0xee, 0xed, 0x2c, 0xfc, 0x8b, 0x59, 0x2c, 0x1c, 0x12, 0xf2, 0x08, 0x5d, 0xbc, 0x85, 0x87, 0xb7, 0x89, 0x76, 0xd0, 0x38, 0x80, 0xa8, 0x2a, 0xab, 0xb1, 0xab, 0x5c, 0x3c, 0x9c, 0xce, 0x11, 0x87, 0x51, 0x0c, 0xff, 0x43, 0xc1, 0xfc, 0x64, 0xaa, 0xa8, 0xf6, 0xbb, 0xda, 0xba, 0x15, 0x3a, 0x80, 0x98, 0xf2, 0x98, 0xf8, 0x94, 0xdb, 0x25, 0x3a, 0x68, 0x86, 0x44, 0xda, 0x44, 0xa5, 0xd8, 0xce, 0x7b, 0xdb, 0xd7, 0x65, 0x31, 0x44, 0xdb, 0x57, 0xe3, 0x92, 0xca, 0xb2, 0xca, 0x5f, 0x48, 0x2c, 0x6c, 0xbe, 0x81, 0x7f, 0x3e, 0x88, 0x04, 0x8e, 0x3b, 0x39, 0xb5, 0x59, 0xda, 0x36, 0x6e, 0x97, 0x63, 0x3d, 0xb2, 0x0e, 0xb7, 0x4a, 0xfd, 0x9e, 0xa8, 0x68, 0x84, 0x0f, 0x59, 0xf1, 0xd9, 0x3c, 0x22, 0xbe, 0x8a, 0x61, 0x32, 0x00, 0x38, 0xe7, 0x41};
			rsaParams.P = new byte[] {0xfd, 0x91, 0xeb, 0xe6, 0x03, 0xe0, 0x54, 0x98, 0xbc, 0x84, 0x99, 0xc1, 0x32, 0xf0, 0xae, 0x2c, 0x97, 0x21, 0xf6, 0xee, 0xe6, 0x88, 0xa9, 0x37, 0x86, 0xb0, 0x09, 0xb1, 0x11, 0x01, 0x01, 0x46, 0x25, 0xa7, 0x64, 0x2f, 0x7e, 0x86, 0x50, 0xe8, 0xc1, 0x05, 0x51, 0xb5, 0x38, 0x0f, 0xcd, 0x21, 0xcc, 0x0d, 0xea, 0xd7, 0x56, 0x24, 0xe2, 0xd7, 0xb9, 0x41, 0xf5, 0x6e, 0x66, 0x4d, 0xd4, 0xa3};
			rsaParams.Q = new byte[] {0xdd, 0x62, 0x1d, 0x7d, 0x2a, 0xa9, 0x00, 0x78, 0x82, 0xb9, 0xf8, 0x22, 0x99, 0xf9, 0x30, 0xd1, 0xee, 0x73, 0x4a, 0xca, 0x90, 0x88, 0x18, 0x04, 0x6f, 0x90, 0x68, 0x3a, 0xad, 0x5f, 0x26, 0xb4, 0x6a, 0xb2, 0x2c, 0xea, 0x0b, 0x0c, 0xf5, 0x74, 0x6e, 0x3e, 0x41, 0x7d, 0xcd, 0xc8, 0xf4, 0xe3, 0x76, 0xbb, 0x3f, 0x6f, 0x43, 0xe0, 0xbb, 0x9d, 0x05, 0x26, 0xd2, 0xfe, 0x09, 0x79, 0xc4, 0xdd};
			rsaParams.DP = new byte[] {0xcf, 0x4b, 0xa0, 0xe7, 0x6c, 0xd9, 0xd3, 0x2e, 0xfe, 0x47, 0x05, 0x0f, 0x8d, 0x9e, 0x77, 0x35, 0x9a, 0xe4, 0x38, 0x64, 0x3b, 0xf1, 0x13, 0x2d, 0x82, 0x9d, 0x9d, 0x7e, 0xb4, 0xe0, 0xf6, 0x72, 0xab, 0x4b, 0xba, 0x3a, 0x9d, 0x9c, 0x1e, 0xbe, 0xf9, 0x35, 0x69, 0x03, 0xd6, 0x6e, 0x0c, 0x8c, 0x09, 0xae, 0x83, 0x03, 0x41, 0xb8, 0x6b, 0xfe, 0x61, 0xc4, 0x4b, 0x69, 0xd2, 0x96, 0xe4, 0x33};
			rsaParams.DQ = new byte[] {0xaf, 0x30, 0x73, 0x91, 0x97, 0x6e, 0xc1, 0xf6, 0x9b, 0xcc, 0xba, 0xf5, 0xf6, 0xce, 0xe1, 0xb9, 0x5f, 0x6f, 0x51, 0x22, 0x57, 0x99, 0xbb, 0x17, 0xd7, 0x89, 0x79, 0x51, 0xe5, 0xdc, 0xc4, 0x6e, 0x45, 0x78, 0xd6, 0x5e, 0x27, 0x7c, 0x8b, 0xc9, 0x25, 0x6c, 0x92, 0xbb, 0x11, 0x5c, 0x13, 0x9e, 0xe5, 0x58, 0x6c, 0x6c, 0x8a, 0x54, 0x8f, 0x63, 0x44, 0xae, 0x62, 0x8d, 0xb1, 0xc5, 0xf0, 0xe9};
			rsaParams.InverseQ = new byte[] {0xbe, 0x61, 0x44, 0x09, 0xf5, 0x29, 0xfb, 0xf1, 0x0b, 0x1a, 0x44, 0xf6, 0x01, 0xc4, 0xb1, 0x99, 0xd3, 0xb3, 0x3f, 0x69, 0x2a, 0x70, 0xa0, 0x70, 0x94, 0xf2, 0x4c, 0x5f, 0x60, 0x38, 0x65, 0x0e, 0xda, 0xaa, 0xad, 0x65, 0x46, 0xa0, 0x5c, 0x3b, 0xff, 0x6a, 0x8c, 0x3f, 0x0f, 0x33, 0xc3, 0xa1, 0xb0, 0x2b, 0xc5, 0x0c, 0x09, 0x32, 0xd5, 0x2f, 0x08, 0x95, 0xda, 0xa5, 0x5b, 0xf8, 0x1a, 0xb2};
			rsa.ImportParameters (rsaParams);
			signAlgo = rsa;
#endif

			CipherSuiteSelector selector = new CipherSuiteSelector (cert);
			X509Certificate[] certs = new X509Certificate[] { cert };
			Socket server = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			server.Bind (new IPEndPoint (IPAddress.Any, 443));
			server.Listen (8);

			while (true) {
				Socket client = null;
				try {
					client = server.Accept ();
					using (NetworkStream nstrm = new NetworkStream (client, FileAccess.ReadWrite, true))
					using (TLSServerStream strm = new TLSServerStream (nstrm, true, certs, signAlgo, selector)) {
						byte[] raw = new byte[8192];
						int recvLen = strm.Read (raw, 0, 4);
						while (true) {
							if (raw[recvLen - 4] == '\r' && raw[recvLen - 3] == '\n' && raw[recvLen - 2] == '\r' && raw[recvLen - 1] == '\n')
								break;
							raw[recvLen ++] = (byte)strm.ReadByte ();
						}
						Console.WriteLine (System.Text.Encoding.ASCII.GetString (raw, 0, recvLen - 2));
						raw = System.Text.Encoding.UTF8.GetBytes ("HTTP/1.0 200 OK\r\nContent-Type: text/html; charset=utf-8\r\nConnection: close\r\n\r\n" +
							"<html><body><h1>Hello ECC World !</h1><p>楕円曲線暗号の世界へようこそ！</p>" +
							"<p>このメッセージはECDSA(secp256r1)によってサーバを検証後、<br />" +
							"ECDH(secp256r1)によって共有した鍵を利用して、<br />" +
							"AES 256bitで暗号化されています</p></body></html>\r\n");
						strm.Write (raw, 0, raw.Length);
					}
				} catch (IOException) {
				} catch {}
			}
		}
	}
}
