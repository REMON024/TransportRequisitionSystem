import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

const CRYPTOGRAPHY_KEY = '8080808080808080';

@Injectable({
  providedIn: 'root'
})
export class CryptoService {

  constructor() { }


  public padOrTruncate(str: string): string {
    var result: string = '';
    if (str.length % 32 == 0)
      return str;

    result = str + '';
    while (!(result.length % 32 == 0)) {
      result = result + " ";
    }

    return result;
  }

  public encrypt(plain_text: string): Promise<string> {

    var key = CryptoJS.enc.Utf8.parse(CRYPTOGRAPHY_KEY);
    var iv = CryptoJS.enc.Utf8.parse(CRYPTOGRAPHY_KEY);

    var ciphertext = CryptoJS.AES.encrypt(plain_text, key, 
      {  
      keySize: 128 / 8,  
      iv: iv,  
      mode: CryptoJS.mode.CBC,  
      padding: CryptoJS.pad.Pkcs7  
   });

    

     console.log(ciphertext.toString());

    return Promise.resolve(ciphertext.toString());
  }

  public decrypt(encrypted_text: string): string {
    var bytes = CryptoJS.AES.decrypt(encrypted_text , CRYPTOGRAPHY_KEY);
    var plain_Text = bytes.toString(CryptoJS.enc.Utf8);

    if (plain_Text === '') {
      console.log('Problem Occured');
      return plain_Text;
    } else {
      return plain_Text;
    }
  }
}
