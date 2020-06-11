//
//  VerificationViewController.swift
//  ERent
//
//  Created by Rexxer on 5/13/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import SVProgressHUD
import Alamofire
 
class VerificationViewController: UIViewController ,UITextFieldDelegate{

    
    var countDown:Int = 60
    var timer:Timer?
    @IBOutlet weak var countDownLbl: UILabel!
    
    var IS_RESEND_CODE : Bool?
    @IBOutlet weak var submitButton: UIButton!
    @IBOutlet weak var verificationCodeTextField: UITextField!
    override func viewDidLoad() {
        super.viewDidLoad()
        self.navigationController?.isNavigationBarHidden = true
        
        // Do any additional setup after loading the view.
        verificationCodeTextField.delegate = self
        
        
        submitButton.layer.cornerRadius = 0.5 * submitButton.bounds.size.height
        submitButton.clipsToBounds = true
        
//        if (IS_RESEND_CODE == true) {
//            resendCode()
//
//        }else {
//            startTimer()
//        }
        
        startTimer()
        
    }

    func resendCode() {
        let credentialData = "a.obeidat1991@gmail.com:Passw0rd!@#".data(using: String.Encoding.utf8)!
        let base64Credentials = credentialData.base64EncodedString(options: [])
        
        var currentToken : String = ""
        if (UserDefaults.standard.string(forKey: "Token") ?? "" != "") {
            currentToken = UserDefaults.standard.string(forKey: "Token") ?? ""
        }else {
            currentToken = UserDefaults.standard.string(forKey: "VerifyToken") ?? ""
        }
        
        Alamofire
            .request("http://rentoservice.ashhalan.com/api/Account/ResendCode",
                     method: .post,
                     parameters: [
                        "ApplicationKey":"Test",
                        "Language": (UserDefaults.standard.string(forKey: "language") ?? "").contains("ar") ? 0: 1,
                        "Token": currentToken
                ],
                     encoding: JSONEncoding.default,
                     headers: ["Authorization": "Basic \(base64Credentials)",
                        "ApplicationKey" : "UEtTXCszXTQhYg1jXWMzNVxk2bgw2JE22aY42YxjUjXZtzTZvGXZsjPZrjHZpDnZjGQ=",
                               "ClientId" : "34D0EBA00FB842D08055E5E2B97655508C28F55C684744FD9B1643F937794F3B",
                               "SecretKey" : "UURcXAAweGU0ZlMzRmFmYQZiczQcOTBhc2FeOGc0NTchOAgyAjhWMDdkeGZVYXplZ2VkYjRmb2QuZns4IGZvYgYwZWVCZkUzZmFbYVdiOTRwOXNhEGEDOHY0KDdYOE8yVzhwMEVkA2YcYSFldmV2YmBmWGQBZnU4H2YBYmswVGVlZkwzdmEuYXBiITQ="])
            .validate()
            .responseJSON { response in
                
                let json = response.result.value as? JSONDictionary
                
                if (json!["ErrorCode"] as! Int == 0) {
                    
                    self.startTimer()
                    
                        SVProgressHUD.showSuccess(withStatus: "code_resent_successfully".localized)
                    
                }else {
                    self.stopTimer()
                    let message : String = json!["Message"] as! String
                     SVProgressHUD.showError(withStatus: message)
                }
        }
        
    }
    
    @objc func updateTimer() {
        if(countDown > 0) {
            countDown = countDown - 1
            countDownLbl.text = String(countDown)
        }else{
            self.resendCode()
        }
    }
    
    
    func startTimer() {
        stopTimer()
        timer = Timer.scheduledTimer(timeInterval: 1.0, target: self, selector: #selector(updateTimer), userInfo: nil, repeats: true)
    }
    

    
    func stopTimer(){
        if timer != nil {
            timer?.invalidate()
            timer = nil
        }
        countDown = 60
        countDownLbl.text = ""
        
    }
    
    override func viewDidDisappear(_ animated: Bool) {
        super.viewDidDisappear(true)
        stopTimer()
    }
    
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(true)
        stopTimer()
    }


    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
    
            view.endEditing(true)
            submitAction(submitButton)
            return true
    }
    
    
    @IBAction func submitAction(_ sender: UIButton) {
        
        stopTimer()
        if verificationCodeTextField.text == nil || verificationCodeTextField.text?.count == 0 {
            SVProgressHUD.showError(withStatus: "verification_code_error".localized)
            return
        }
        
        SVProgressHUD.show()
        
        let code = verificationCodeTextField.text! as NSString
    
        APIClient.execute(request: VerificationRequest(code: code.integerValue)) {  [weak self] result in
            SVProgressHUD.dismiss()
            switch result {
            case .success(_):
                
                var currentToken : String = ""
                if (UserDefaults.standard.string(forKey: "Token") ?? "" != "") {
                    currentToken = UserDefaults.standard.string(forKey: "Token") ?? ""
                }else {
                    currentToken = UserDefaults.standard.string(forKey: "VerifyToken") ?? ""
                }
                UserDefaults.standard.set("true", forKey: "IS_REGISTERED")
                UserDefaults.standard.set(currentToken, forKey: "Token")
                
                let viewController:UIViewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "VerificationSuccessViewController") as UIViewController
                
                self?.navigationController?.pushViewController(viewController, animated: true)
            case .failure(let error):
                self?.displayAlert(message: error)
            }
        }
        
        
    }

    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        view.endEditing(true)
    }
    override func touchesEnded(_ touches: Set<UITouch>, with event: UIEvent?) {
        view.endEditing(true)
    }
    override func touchesMoved(_ touches: Set<UITouch>, with event: UIEvent?) {
        
    }
}
