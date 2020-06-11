//
//  ContactUsViewController.swift
//  ERent
//
//  Created by Zaid najjar on 7/1/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import Alamofire
import SVProgressHUD

class ContactUsViewController: UIViewController {


    @IBOutlet weak var subjectEdt: UITextField!
    
    @IBOutlet weak var bodyEdt: UITextField!
    
    @IBOutlet weak var nameEdt: UITextField!
    
    @IBOutlet weak var emailEdt: UITextField!
    
    @IBOutlet weak var phoneEdt: UITextField!
    
    @IBOutlet weak var sendBtn: UIButton!
    
    
    @IBAction func sendAction(_ sender: Any) {
        if validate(){
            sendForm()
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
        
        sendBtn.layer.cornerRadius = 0.5 * sendBtn.bounds.size.height
        sendBtn.clipsToBounds = true
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    func validate() -> Bool {
        if subjectEdt.text == nil || subjectEdt.text?.count == 0 {
            SVProgressHUD.showError(withStatus: "subject_error".localized)
            return false
        }
        if bodyEdt.text == nil || bodyEdt.text?.count == 0 {
            SVProgressHUD.showError(withStatus: "body_error".localized)
            return false
        }
        if nameEdt.text == nil || nameEdt.text?.count == 0 {
            SVProgressHUD.showError(withStatus: "name_error".localized)
            return false
        }
        if emailEdt.text == nil || emailEdt.text?.count == 0 {
            SVProgressHUD.showError(withStatus: "email_error".localized)
            return false
        }
        if phoneEdt.text == nil || phoneEdt.text?.count == 0 {
            SVProgressHUD.showError(withStatus: "phone_error2".localized)
            return false
        }
        return true
    }
    
    func sendForm () {
        SVProgressHUD.show()
        let request = ContactUsRequest(subject: subjectEdt.text!, body: bodyEdt.text!, name: nameEdt.text!, email: emailEdt.text!, phone: phoneEdt.text!)
        APIClient.execute(request: request) { [weak self] result in
            switch result {
            case .success():
                SVProgressHUD.dismiss()
                print(result)
                SVProgressHUD.showSuccess(withStatus: "Your messages has been sent successfully.".localized)
            case .failure(let error):
                 SVProgressHUD.dismiss()
                print(error)
            }
        }
        
    }
    
    

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */

}
