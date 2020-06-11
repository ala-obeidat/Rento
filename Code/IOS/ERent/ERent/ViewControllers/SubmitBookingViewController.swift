//
//  SubmitBookingViewController.swift
//  ERent
//
//  Created by Rexxer on 6/2/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import CoreLocation
import SVProgressHUD

import Alamofire
class SubmitBookingViewController: UIViewController , UITextFieldDelegate , CLLocationManagerDelegate{

    
    @IBOutlet weak var toolBar: UIToolbar!
    
    
    @IBAction func toolBarAction(_ sender: UIBarButtonItem) {
        
        toolBar.isHidden = true
        datePicker.isHidden = true
        
        
        let dateFormatter = DateFormatter()
        dateFormatter.dateFormat = "MM/dd/yyyy"// HH:mm:ss"
        dateFormatter.locale = NSLocale(localeIdentifier: "EN") as Locale?
        let somedateString = dateFormatter.string(from: datePicker.date)
        if selectedTextField == "from" {
            fromDateTextField.text = somedateString
            fromDate = datePicker.date
        }else {
            toDateValueChanged.text = somedateString
            toDate = datePicker.date
        }

        
    }
    var car : CarModel? = nil
    
    var selectedTextField = ""
    var longitude : Double = 0
    var latitude : Double = 0
    
    var pickupMethod = 0;
    var didCheckUserAgreement = false
    
    var fromDate = Date()
    var toDate = Date()
    
    @IBOutlet weak var datePicker: UIDatePicker!
    
    @IBOutlet weak var confirmBookingButton: UIButton!
    
    
    @IBOutlet weak var pickupMethodSegment: UISegmentedControl!
    
    
    @IBOutlet weak var fromDateTextField: UITextField!
    
    @IBOutlet weak var toDateValueChanged: UITextField!
    
    @IBAction func pickupMethodValueChanged(_ sender: UISegmentedControl) {
        
        pickupMethod = sender.selectedSegmentIndex
        
    }
    
    
    
    
    @IBOutlet weak var userAgreementButton: UIButton!
    
    @IBAction func datePickerValueChanged(_ sender: UIDatePicker) {
        
//        let dateFormatter = DateFormatter()
//        dateFormatter.dateFormat = "MMM dd, YYYY"
//        let somedateString = dateFormatter.string(from: sender.date)
//        if selectedTextField == "from" {
//            fromDateTextField.text = somedateString
//            fromDate = sender.date
//        }else {
//            toDateValueChanged.text = somedateString
//            toDate = sender.date
//        }
//        datePicker.isHidden = true
    
        
    }
    
    @IBAction func confirmBookingAction(_ sender: UIButton) {
        if !validate() {
             return
        }
        SVProgressHUD.show()
        var price : Double = 0.0
        var numberOfDays = fromDate.timeIntervalSince(toDate) / (60 * 60 * 24)
        if (numberOfDays < 0) {
            numberOfDays = numberOfDays * -1
        }
        if numberOfDays < 7 {
            price = Double(car!.dayCost)! * numberOfDays
        }else if numberOfDays < 7 {
                price = Double(car!.weekCost) * numberOfDays
        }else {
            price = Double(car!.monthCost) * numberOfDays
        }
        
//        let request = CheckOutRequest(carId: Int((car?.id)!) ?? 0, price: Int(price), from: fromDate , to: toDate, flag: pickupMethodSegment.selectedSegmentIndex, longitude: longitude, latitude: latitude)
        
        let cityId = UserDefaults.standard.value(forKey: "CityId") as? Int ?? 1
        let request = CheckOutRequest(carId: Int((car?.id)!) ?? 0, cityId: cityId, price: Int(price), from: fromDateTextField.text!, to: toDateValueChanged.text!, flag: pickupMethodSegment.selectedSegmentIndex, longitude: longitude, latitude: latitude)
        
        APIClient.execute(request: request) { [weak self] result in
            SVProgressHUD.dismiss()
            switch result {
            case .success():
                
                //
                let alertController = UIAlertController(title: "car_booked_successfully".localized , message: nil , preferredStyle: .alert)
                
                // Create the actions
                let okAction = UIAlertAction(title: "done".localized, style: UIAlertActionStyle.default) {
                    UIAlertAction in
                    self?.navigationController?.popToRootViewController(animated: true)
                }
                // Add the actions
                alertController.addAction(okAction)
              
                
                // Present the controller
                self?.present(alertController, animated: true, completion: nil)
                //
                
               
              break
            case .failure(let error):
                self?.displayAlert(message: error)
            }
        }
        
    }
    
    
    @IBAction func userAgreementAction(_ sender: UIButton) {
        if didCheckUserAgreement {
            userAgreementButton.setImage(UIImage(named: "ic_green_empty_checkbox"), for: .normal)
            didCheckUserAgreement = false
        }else{
            
            userAgreementButton.setImage(UIImage(named: "ic_green_filled_checkbox"), for: .normal)
            didCheckUserAgreement = true
        }
    }
    
    
    
    
    let locationManager = CLLocationManager()
    
    
    @IBOutlet weak var agreementLabel: UILabel!
    override func viewDidLoad() {
        super.viewDidLoad()
        fromDateTextField.delegate = self
        toDateValueChanged.delegate = self
        
        
        agreementLabel.attributedText = NSAttributedString(string: "accept_agreement".localized, attributes:            [kCTUnderlineStyleAttributeName as NSAttributedStringKey: NSUnderlineStyle.styleSingle.rawValue])
        
        
        
        confirmBookingButton.layer.cornerRadius = confirmBookingButton.frame.height/2
        confirmBookingButton.clipsToBounds = true
        
        datePicker.isHidden = true
        toolBar.isHidden = true
        
           if (car?.officeFlag == "1") {
       
            locationManager.requestAlwaysAuthorization()
            
            // For use in foreground
            locationManager.requestWhenInUseAuthorization()
            
            if CLLocationManager.locationServicesEnabled() {
                locationManager.delegate = self
                locationManager.desiredAccuracy = kCLLocationAccuracyNearestTenMeters
                locationManager.startUpdatingLocation()
            }
        }
        
        if (car?.officeFlag == "0") {
            self.pickupMethodSegment.removeSegment(at: 1, animated: false)
        }
        
        // Do any additional setup after loading the view.
    }

    
    func locationManager(_ manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
        let locValue:CLLocationCoordinate2D = manager.location!.coordinate
        print("locations = \(locValue.latitude) \(locValue.longitude)")
        longitude = locValue.longitude
        latitude = locValue.latitude
        
    }
    
    func textFieldShouldBeginEditing(_ textField: UITextField) -> Bool {
        if textField == fromDateTextField {
            selectedTextField = "from"
        }else{
            selectedTextField = "to"
        }
        
    
//        let date = Date()
//        let formatter = DateFormatter()
//        formatter.dateFormat = "MM/dd/yyyy"
//        let result = formatter.string(from: date)
//        let dateValue = formatter.date(from:result)!
//        datePicker.date = dateValue
        
        
        if selectedTextField == "from" {
            let today = Date()
            datePicker.date = today
        }else {
            let today = Date()
            let tomorrow = Calendar.current.date(byAdding: .day, value: 1, to: today)
            datePicker.date = tomorrow!
        }
        
        
        datePicker.isHidden = false
        toolBar.isHidden = false
        return false
    }
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    

    func validate() -> Bool {
        
        if fromDateTextField.text == nil || fromDateTextField.text == "" {
             SVProgressHUD.showError(withStatus:"from_date_error".localized)
            return false
        }
        if toDateValueChanged.text == nil || toDateValueChanged.text == "" {
            SVProgressHUD.showError(withStatus:"to_date_error".localized)
            return false
        }
//        if !didCheckUserAgreement {
//            SVProgressHUD.showError(withStatus:"accept_agreement")
//            return false
//        }
        
        if pickupMethodSegment.selectedSegmentIndex == 1 && (longitude == 0 || latitude == 0) {
            SVProgressHUD.showError(withStatus:"Allow E-Rent to access your location from settings")
            return false
        }
        
        return true
    }
   
    @IBAction func didTapTerms(_ sender: UITapGestureRecognizer) {
        
        let credentialData = "a.obeidat1991@gmail.com:Passw0rd!@#".data(using: String.Encoding.utf8)!
        let base64Credentials = credentialData.base64EncodedString(options: [])
        
        
        
        Alamofire
            .request("http://rentoservice.ashhalan.com/api/Help/GetTermsAndCondition",
                     method: .post,
                     parameters:  [
                        "Data":car?.id ?? "0",
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
                
                let str1 = json!["Data"] as? String ?? "-"
                vc.str = str1
                
                vc.title = "Terms And Conditions".localized
                self.navigationController?.pushViewController(vc, animated: true)
        }
        
        
    }
}
