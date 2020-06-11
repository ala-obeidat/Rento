//
//  SignupViewController.swift
//  ERent
//
//  Created by Rexxer on 5/10/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import SVProgressHUD
import Alamofire

class SignupViewController: UIViewController ,UITextFieldDelegate, UIScrollViewDelegate,FilterViewControllerDelegate ,UIImagePickerControllerDelegate, UINavigationControllerDelegate{
    
    
    @IBOutlet weak var doneButton: UIToolbar!
    
    @IBAction func doneButtonAction(_ sender: UIBarButtonItem) {
        
        let dateFormatter = DateFormatter()
        dateFormatter.dateFormat = "MMM dd, YYYY"
        dateFormatter.locale = NSLocale(localeIdentifier: "EN") as Locale?
        let somedateString = dateFormatter.string(from: datePicker.date)
        birthdateTextField.text = somedateString
        
        doneButton.isHidden = true
        datePicker.isHidden = true
    }
    @IBOutlet weak var containerView: UIView!
    
    @IBOutlet weak var scrollView: UIScrollView!
    
    @IBOutlet weak var titleLabel: UILabel!
    @IBOutlet weak var logoImageView: UIImageView!
    
    @IBOutlet weak var usernameContainer: UIView!
    @IBOutlet weak var usernameTextField: UITextField!
    
    @IBOutlet weak var emailContainer: UIView!
    @IBOutlet weak var emailTextField: UITextField!
    
    @IBOutlet weak var phoneNumberContainer: UIView!
    @IBOutlet weak var phoneNumberTextField: UITextField!
    
    @IBOutlet weak var segmentedGender: UISegmentedControl!
    
    @IBOutlet weak var fullName: UITextField!
    @IBOutlet weak var passwordContainer: UIView!
    @IBOutlet weak var passwordTextField: UITextField!
    
    @IBOutlet weak var birthdateContainer: UIView!
    @IBOutlet weak var birthdateTextField: UITextField!
    
    
    @IBOutlet weak var countryContainer: UIView!
    @IBOutlet weak var countryTextField: UITextField!
    @IBOutlet weak var countryButton: UIButton!
    
    
    @IBOutlet weak var cityContainer: UIView!
    @IBOutlet weak var cityTextField: UITextField!
    @IBOutlet weak var cityButton: UIButton!
    
    
    
    @IBOutlet weak var licenseLabel: UILabel!
    
    
    @IBOutlet weak var profileLabel: UILabel!
    
    @IBOutlet weak var licenseButton: UIButton!
    
    @IBOutlet weak var profileButton: UIButton!
    
    @IBOutlet weak var submitButton: UIButton!
    
    
    
    @IBOutlet weak var datePicker: UIDatePicker!
    
    
    
    @IBOutlet weak var userAgreementContainerView: UIView!
    @IBOutlet weak var userAgreementButton: UIButton!
    @IBOutlet weak var userAgreementLabel: UILabel!
    
    @IBOutlet weak var identifierContainerView: UIView!
    @IBOutlet weak var identifierTextField: UITextField!
    
    var profilePic : UIImage? = nil
    var license : UIImage? = nil
    
    var imagePicker: UIImagePickerController!
    
    var lastSelected = ""
    
    var didCheckUserAgreement = false
    
    
    @IBAction func userAgreementAction(_ sender: UIButton) {
        if didCheckUserAgreement {
            userAgreementButton.setImage(UIImage(named: "ic_checkbox_unselected"), for: .normal)
            didCheckUserAgreement = false
        }else{
            
            userAgreementButton.setImage(UIImage(named: "ic_checkbox_selected"), for: .normal)
            didCheckUserAgreement = true
        }
        
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
    
        userAgreementLabel.attributedText = NSAttributedString(string: "accept_agreement".localized, attributes:  [kCTUnderlineStyleAttributeName as NSAttributedStringKey: NSUnderlineStyle.styleSingle.rawValue])
        
        
        containerView.backgroundColor = UIColor.AppTheme()
        
        countryTextField.isUserInteractionEnabled = false
        cityTextField.isUserInteractionEnabled = false

        registerKeyboardNotifications()
        
        doneButton.isHidden = true
        
        usernameTextField.delegate = self
        emailTextField.delegate = self
        phoneNumberTextField.delegate = self
        passwordTextField.delegate = self
        birthdateTextField.delegate = self
        identifierTextField.delegate = self
        
        scrollView.delegate = self
        
        submitButton.layer.cornerRadius = 0.5 * submitButton.bounds.size.height
        submitButton.clipsToBounds = true
        
        
        licenseButton.layer.cornerRadius = 5.0;
        licenseButton.clipsToBounds = true
        
        profileButton.layer.cornerRadius = 5.0;
        profileButton.clipsToBounds = true
        
        
        datePicker.isHidden = true
        
        datePicker.backgroundColor = .white
        
        segmentedGender.removeSegment(at: 0, animated: false)
        segmentedGender.removeSegment(at: 0, animated: false)
        
        segmentedGender.insertSegment(withTitle: "male".localized, at: 0, animated: false)
        segmentedGender.insertSegment(withTitle: "female".localized, at: 1, animated: false)
        
    }
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        self.navigationController?.isNavigationBarHidden = false
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    @IBOutlet weak var drivingLisenceButton: UIButton!
    
    @IBAction func countryAction(_ sender: UIButton) {
        let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "FilterViewController") as! FilterViewController
        vc.filterType = "Country"
        vc.delegate = self
        navigationController?.pushViewController(vc, animated: true)
        
    }
    @IBAction func cityAction(_ sender: UIButton) {
        let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "FilterViewController") as! FilterViewController
        vc.filterType = "City"
        vc.delegate = self
        navigationController?.pushViewController(vc, animated: true)
        
    }
    
    @IBAction func drivingLisenceAction(_ sender: UIButton) {
    }
    
    @IBAction func idPhotoAction(_ sender: UIButton) {
    }

    @IBAction func submitAction(_ sender: UIButton) {
    
        if !validateFields() {
            return
        }
        
        let alertController = UIAlertController(title: "alert".localized, message: "verify_phone_title".localized + " " +  phoneNumberTextField.text!, preferredStyle: .alert)
        
        // Create the actions
        let okAction = UIAlertAction(title: "confirm".localized, style: UIAlertActionStyle.default) {
            UIAlertAction in
            self.registerUser()
        }
        let cancelAction = UIAlertAction(title: "edit".localized, style: UIAlertActionStyle.cancel) {
            UIAlertAction in
            //request phone focus
            self.phoneNumberTextField.becomeFirstResponder()
        }
        
        // Add the actions
        alertController.addAction(okAction)
        alertController.addAction(cancelAction)
        
        // Present the controller
        self.present(alertController, animated: true, completion: nil)
        
        
    }
    
    func registerUser() {
        let Formatter = NumberFormatter()
        Formatter.locale = NSLocale(localeIdentifier: "EN") as Locale?
        let phoneNumberFormatted = Formatter.number(from: phoneNumberTextField.text!)
        
        
        var gen = 0
        if segmentedGender.selectedSegmentIndex == -1 {
            gen = -2
        }else {
            gen = segmentedGender.selectedSegmentIndex
        }
        let request = RegisterRequest(
            fullname: (fullName.text?.trimmingCharacters(in: .whitespacesAndNewlines))!,
            username: (usernameTextField.text?.trimmingCharacters(in: .whitespacesAndNewlines))!,
            email: (emailTextField.text?.trimmingCharacters(in: .whitespacesAndNewlines))!,
            phoneNumber: (phoneNumberFormatted?.stringValue)!,
            password: (passwordTextField.text?.trimmingCharacters(in: .whitespacesAndNewlines))!,
            identifierId: (identifierTextField.text! as NSString).integerValue,
            licenseId: 1234,
            birthdate: birthdateTextField.text!,
            country: countryTextField.text!,
            city: 1,
            licensePhoto: UIImageJPEGRepresentation(license ?? UIImage(), 0.5) ?? Data(),
            profilePhoto: UIImageJPEGRepresentation(profilePic ?? UIImage(), 0.5) ?? Data(),
            gender: gen
        )
        
        SVProgressHUD.show()
        
        APIClient.execute(request: request) { [weak self] result in
            SVProgressHUD.dismiss()
            switch result {
            case .success(_):
                let viewController:UIViewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "VerificationViewController") as UIViewController
                //        self.present(viewController, animated: true, completion: nil)
                
                self?.navigationController?.pushViewController(viewController, animated: true)
                
            case .failure(let error):
                self?.displayAlert(message: error)
            }
        }
    }
    

    //MARK: - tfdelegate
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        if (textField == usernameTextField) {
            emailTextField.becomeFirstResponder()
        } else if (textField == emailTextField) {
            phoneNumberTextField.becomeFirstResponder()
        } else if (textField == phoneNumberTextField) {
            passwordTextField.becomeFirstResponder()
        }else if (textField == passwordTextField){
            identifierTextField.becomeFirstResponder()
        }else{
            view.endEditing(true)
        }
        return true
    }
    
    
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        view.endEditing(true)
    }
    override func touchesEnded(_ touches: Set<UITouch>, with event: UIEvent?) {
        view.endEditing(true)
    }
    
    
    func registerKeyboardNotifications() {
        NotificationCenter.default.addObserver(self, selector: #selector(keyboardWasShown), name: .UIKeyboardDidShow, object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(keyboardWillBeHidden), name: .UIKeyboardWillHide, object: nil)
    }
    
    @objc func keyboardWasShown(notification: NSNotification) {
        if let keyboardFrame: NSValue = notification.userInfo?[UIKeyboardFrameEndUserInfoKey] as? NSValue {
            let keyboardRectangle = keyboardFrame.cgRectValue
            let keyboardHeight = keyboardRectangle.height
            
            let contentInsets = UIEdgeInsets(top: 0, left: 0, bottom: keyboardHeight, right: 0)
            self.scrollView.contentInset = contentInsets
            self.scrollView.scrollIndicatorInsets = contentInsets
        }
    }
    
    @objc func keyboardWillBeHidden(notification: NSNotification) {
        let contentInsets = UIEdgeInsets.zero
        self.scrollView.contentInset = contentInsets
        self.scrollView.scrollIndicatorInsets = contentInsets
    }
    
    func didSelectItemView(_ filterType : String , item : BaseModel) {
        if filterType == "Country" {
            countryTextField.text = item.name
        }else if filterType == "City"{
            cityTextField.text = item.name
            
        }
    }
    
    
    @IBAction func licenseAction(_ sender: UIButton) {
        lastSelected = "license"
     showPicker()
    }
    
    @IBAction func ProfileAction(_ sender: UIButton) {
        lastSelected = "profile"
     showPicker()
        
    }
    
    
    func showPicker(){
        let actionSheetController: UIAlertController = UIAlertController(title: "Add Image".localized, message: nil, preferredStyle: .actionSheet)
        
        let cancelActionButton = UIAlertAction(title: "Cancel".localized, style: .cancel) { _ in
            print("Cancel")
        }
        actionSheetController.addAction(cancelActionButton)
        
        let saveActionButton = UIAlertAction(title: "Take Photo".localized, style: .default)
        { _ in
            
            if  UIImagePickerController.isSourceTypeAvailable(.camera){
                self.imagePicker =  UIImagePickerController()
                self.imagePicker.delegate = self
                self.imagePicker.sourceType = .camera
                
                self.present(self.imagePicker, animated: true, completion: nil)
            }
        }
        actionSheetController.addAction(saveActionButton)
        
        let deleteActionButton = UIAlertAction(title: "Select from library".localized, style: .default)
        { _ in
            if UIImagePickerController.isSourceTypeAvailable(.savedPhotosAlbum){
                print("Button capture")
                self.imagePicker =  UIImagePickerController()
                self.imagePicker.delegate = self
                self.imagePicker.sourceType = .savedPhotosAlbum;
                self.imagePicker.allowsEditing = false
                
                self.present(self.imagePicker, animated: true, completion: nil)
            }
            
        }
        actionSheetController.addAction(deleteActionButton)
        
        
        actionSheetController.popoverPresentationController?.sourceView = self.view
        actionSheetController.popoverPresentationController?.sourceRect = CGRect(x:self.view.bounds.size.width / 2.0,y: self.view.bounds.size.height / 2.0, width:1.0,height: 1.0)
        
        
        self.present(actionSheetController, animated: true, completion: nil)

    }
    
    func imagePickerController(picker: UIImagePickerController!, didFinishPickingImage image: UIImage!, editingInfo: NSDictionary!){
        if self.lastSelected == "license" {
            self.licenseButton.setImage(image, for: .normal)
        } else {
            self.profileButton.setImage(image, for: .normal)
        }
    }
   
    func imagePickerController(_ picker: UIImagePickerController, didFinishPickingMediaWithInfo info: [String : Any]) {
        let selectedImage = info[UIImagePickerControllerOriginalImage] as! UIImage
        
        if self.lastSelected == "license" {
            self.licenseButton.setImage(nil, for: .normal)
            self.licenseButton.setBackgroundImage(selectedImage, for: .normal)
            license = selectedImage
        } else {
            self.profileButton.setImage(nil, for: .normal)
           self.profileButton.setBackgroundImage(selectedImage, for: .normal)
            profilePic = selectedImage
        }

        // Dismiss the picker.
        dismiss(animated: true, completion: nil)
    }
    
    @IBAction func didTapBirthDate(_ sender: UITapGestureRecognizer) {
        datePicker.isHidden = false
        doneButton.isHidden = false
        
        birthdateTextField.inputView = datePicker
    }
    
    @IBAction func datePickerValueChanged(_ sender: UIDatePicker) {
        
    
    }
 
    
    func validateFields() -> Bool {
        
        if usernameTextField.text == nil || usernameTextField.text?.count == 0 {
            SVProgressHUD.showError(withStatus: "username_error".localized)
            return false
        }
        let emailText = emailTextField.text?.trimmingCharacters(in: .whitespacesAndNewlines)
        
        if emailText == nil || !(emailText!.isValidEmail()) {
            SVProgressHUD.showError(withStatus: "email_error".localized)
            return false
        }
//        if phoneNumberTextField.text == nil || phoneNumberTextField.text?.count != 10 || !(phoneNumberTextField.text?.hasPrefix("05"))! {
        if phoneNumberTextField.text == nil || phoneNumberTextField.text?.count == 0{
            SVProgressHUD.showError(withStatus: "phone_error".localized)
            return false
        }
        
        if passwordTextField.text == nil || passwordTextField.text?.count == 0{
            SVProgressHUD.showError(withStatus: "password_error".localized)
            return false
        }
        
        if fullName.text == nil || fullName.text?.count == 0{
            SVProgressHUD.showError(withStatus: "fullname_error".localized)
            return false
        }
        
//        if identifierTextField.text == nil || identifierTextField.text?.count != 10 {
//
//            SVProgressHUD.showError(withStatus: "identifier_error".localizedLowercase)
//            return false
//        }
//        if birthdateTextField.text == nil  || birthdateTextField.text?.count == 0 {
//
//            SVProgressHUD.showError(withStatus:"birth_error".localizedLowercase)
//            return false
//        }
        
        
//        if countryTextField.text == nil || countryTextField.text?.count == 0 {
//
//            SVProgressHUD.showError(withStatus:"country_error".localizedLowercase)
//            return false
//        }
//
//        if cityTextField.text == nil  || cityTextField.text?.count == 0 {
//
//            SVProgressHUD.showError(withStatus:"city_error".localizedLowercase)
//            return false
//        }
        
//        if license == nil {
//            SVProgressHUD.showError(withStatus:"add_licence_photo".localizedLowercase)
//            return false
//        }
//
//        if profilePic == nil {
//            SVProgressHUD.showError(withStatus:"add_identifier_photo".localizedLowercase)
//            return false
//        }
        
        if !didCheckUserAgreement {
            SVProgressHUD.showError(withStatus:"accept_agreement".localized)
            return false
        }
        return true
        
    }
    @IBAction func cityFieldTapped(_ sender: UITapGestureRecognizer) {
        let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "FilterViewController") as! FilterViewController
        vc.filterType = "City"
        vc.delegate = self
        
        vc.title = "Select City".localized
        navigationController?.pushViewController(vc, animated: true)
        
    }
    @IBAction func countryFieldTapped(_ sender: UITapGestureRecognizer) {
        
        let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "FilterViewController") as! FilterViewController
        vc.filterType = "Country"
        vc.delegate = self
        
        vc.title = "Select Country".localized
        navigationController?.pushViewController(vc, animated: true)
    }
    
    
    func termsRequest() {
        //Help/GetTermsAndCondition
        
        
        let credentialData = "a.obeidat1991@gmail.com:Passw0rd!@#".data(using: String.Encoding.utf8)!
        let base64Credentials = credentialData.base64EncodedString(options: [])
        
        
        Alamofire
            .request("http://rentoservice.ashhalan.com/api/Help/GetTermsAndCondition",
                     method: .post,
                parameters: [
                    "Data":"0",
                    "ApplicationKey":"Test",
                    "Language": (UserDefaults.standard.string(forKey: "language") ?? "").contains("ar") ? 0: 1,
                    "Token": UserDefaults.standard.string(forKey: "Token") ?? ""
                ],
                     encoding: JSONEncoding.default,
                     headers: ["Authorization": "Basic \(base64Credentials)",
                        "ApplicationKey" : "UEtTXCszXTQhYg1jXWMzNVxk2bgw2JE22aY42YxjUjXZtzTZvGXZsjPZrjHZpDnZjGQ=",
                        "ClientId" : "34D0EBA00FB842D08055E5E2B97655508C28F55C684744FD9B1643F937794F3B",
                        "SecretKey" : "UURcXAAweGU0ZlMzRmFmYQZiczQcOTBhc2FeOGc0NTchOAgyAjhWMDdkeGZVYXplZ2VkYjRmb2QuZns4IGZvYgYwZWVCZkUzZmFbYVdiOTRwOXNhEGEDOHY0KDdYOE8yVzhwMEVkA2YcYSFldmV2YmBmWGQBZnU4H2YBYmswVGVlZkwzdmEuYXBiITQ="])
            .validate()
            .responseJSON { response in
                
                
            let json = response.result.value as? JSONDictionary
            
                let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "TermsAndConditionsViewController") as! TermsAndConditionsViewController
                
                let str1 = json!["Data"] as? String ?? ""
                vc.str = str1
                
                vc.title = "Terms And Conditions".localized
                self.navigationController?.pushViewController(vc, animated: true)
//                TermsAndConditionsViewController
        }
        
    }
    
    @IBAction func openAgreement(_ sender: UITapGestureRecognizer) {
        termsRequest()
    }
    
    func ggwp(id:Int , name:String , completionHandler:@escaping (_ data:JSONDecoder , _ errorType:String , _ errorMessage:String) -> Void ) {
        
    }
}
