//
//  ResetPasswordViewController.swift
//  ERent
//
//  Created by Rexxer on 5/28/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import SVProgressHUD
class ResetPasswordViewController: UIViewController , UITextFieldDelegate {

    @IBOutlet weak var veificationCodeTextfield: UITextField!
    
    @IBOutlet weak var passwordTextfield: UITextField!
    
    @IBOutlet weak var confirmTextField: UITextField!
    
    @IBOutlet weak var sendButton: UIButton!
    
    
    @IBAction func sendAction(_ sender: UIButton) {
        
        if validatePassword() {
            //call reset password api
            
            let verification :Int? = Int(veificationCodeTextfield.text!)
            let request = ResetPasswordRequest(code: verification! , newPassword: passwordTextfield.text!)
            
            SVProgressHUD.show()
            
            APIClient.execute(request: request) { [weak self] result in
                SVProgressHUD.dismiss()
                switch result {
                case .success(_):
                    let viewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "LoginViewController") as UIViewController
                    self?.present(viewController, animated: true, completion: nil)
                case .failure(let error):
                    self?.displayAlert(message: error)
                }
            }
        }
        
        
    }
    
    func validatePassword() -> Bool {
        if passwordTextfield.text != confirmTextField.text {
            SVProgressHUD.showError(withStatus: "passwords_error".localized)
        
            return false
        }
        return true
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

        sendButton.layer.cornerRadius = sendButton.frame.size.height/2
        sendButton.clipsToBounds = true
        
        
        veificationCodeTextfield.delegate = self
        passwordTextfield.delegate = self
        confirmTextField.delegate = self
        
        
        // Do any additional setup after loading the view.
    }

    
    //MARK: - tfdelegate
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        if (textField == veificationCodeTextfield) {
            passwordTextfield.becomeFirstResponder()
        } else if (textField == passwordTextfield) {
            confirmTextField.becomeFirstResponder()
        }else{
            view.endEditing(true)
        }
        return true
    }
    

 
}
