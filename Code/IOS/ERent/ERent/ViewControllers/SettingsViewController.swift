//
//  SettingsViewController.swift
//  ERent
//
//  Created by Rexxer on 5/17/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class SettingsViewController: UIViewController {

    @IBOutlet weak var logout: UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()
        changeLanguageButton.layer.cornerRadius = changeLanguageButton.frame.height/2
        changeLanguageButton.clipsToBounds = true
        
    
        // Do any additional setup after loading the view.
        
        let token : String = UserDefaults.standard.object(forKey: "IS_REGISTERED") as? String ?? ""
        if token == "" {
            logout.setTitle("login".localized, for: .normal)
            logout.backgroundColor = hexStringToUIColor(hex: "#10bd41")
        }else {
            logout.setTitle("logout".localized, for: .normal)
            logout.backgroundColor = hexStringToUIColor(hex: "#e61a4f")
        }
        let rec = UserDefaults.standard.bool(forKey: "SHOW_NOTI_ERENT")
        if rec {
            notificationSwitch.isOn = true
        }else {
            notificationSwitch.isOn = false
        }
    }
    
   
    
    func hexStringToUIColor (hex:String) -> UIColor {
        var cString:String = hex.trimmingCharacters(in: .whitespacesAndNewlines).uppercased()
        
        if (cString.hasPrefix("#")) {
            cString.remove(at: cString.startIndex)
        }
        
        if ((cString.count) != 6) {
            return UIColor.gray
        }
        
        var rgbValue:UInt32 = 0
        Scanner(string: cString).scanHexInt32(&rgbValue)
        
        return UIColor(
            red: CGFloat((rgbValue & 0xFF0000) >> 16) / 255.0,
            green: CGFloat((rgbValue & 0x00FF00) >> 8) / 255.0,
            blue: CGFloat(rgbValue & 0x0000FF) / 255.0,
            alpha: CGFloat(1.0)
        )
    }
    

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBOutlet weak var notificationSwitch: UISwitch!
    
    
    @IBAction func logoutActio(_ sender: Any) {
        
        UserDefaults.standard.setValue("", forKey: "Username")
       UserDefaults.standard.setValue("", forKey: "Token")
        UserDefaults.standard.setValue("", forKey: "IS_REGISTERED")
        let storyboard: UIStoryboard = UIStoryboard(name: "Main", bundle: nil)
        let vc: UIViewController = storyboard.instantiateViewController(withIdentifier: "LoginViewController") as! LoginViewController
        //  self.present(vc, animated: true, completion: nil)
        self.navigationController?.pushViewController(vc, animated: true)
        
    }
    
    @IBAction func notificationSwitchValueChanged(_ sender: UISwitch) {
//                enable
//        if sender.isOn {
//            if #available(iOS 10.0, *) {
//
//                // SETUP FOR NOTIFICATION FOR iOS >= 10.0
//                let center  = NotificationCenter.current()
//                center.delegate = self
//                center.requestAuthorization(options: [.sound, .alert, .badge]) { (granted, error) in
//                    if error == nil{
//                        DispatchQueue.main.async(execute: {
//                            UIApplication.shared.registerForRemoteNotifications()
//                        })
//                    }
//                }
//
//            } else {
//
//                // SETUP FOR NOTIFICATION FOR iOS < 10.0
//
//                let settings = UIUserNotificationSettings(types: [.sound, .alert, .badge], categories: nil)
//                UIApplication.shared.registerUserNotificationSettings(settings)
//
//                UIApplication.shared.registerForRemoteNotifications()
//            }
//        }else {
//
//            //disable
//            UIApplication.shared.unregisterForRemoteNotifications()
//
//        }
        
        if sender.isOn {
            UserDefaults.standard.set(true, forKey: "SHOW_NOTI_ERENT")
        }else {
            UserDefaults.standard.set(false, forKey: "SHOW_NOTI_ERENT")
        }
        
    }
    
    
    @IBOutlet weak var changeLanguageButton: UIButton!
    
    
    @IBAction func changeLanguageAction(_ sender: UIButton) {
        
        
        let alertController = UIAlertController(title: "Change language".localized, message: "App will restart to change language".localized, preferredStyle: .alert)
        
        // Create the actions
        let okAction = UIAlertAction(title: "OK".localized, style: UIAlertActionStyle.default) {
            UIAlertAction in
            if L102Language.currentAppleLanguage() == "en" {
                L102Language.setAppleLAnguageTo(lang: "ar")
                let defaults = UserDefaults.standard
                defaults.set("ar", forKey: "language")
            } else {
                L102Language.setAppleLAnguageTo(lang: "en")
                let defaults = UserDefaults.standard
                defaults.set("en", forKey: "language")
            }
            exit(0);
        }
        let cancelAction = UIAlertAction(title: "cancel".localized, style: UIAlertActionStyle.cancel) {
            UIAlertAction in
            NSLog("Cancel Pressed")
        }
        
        // Add the actions
        alertController.addAction(okAction)
        alertController.addAction(cancelAction)
        
        // Present the controller
        self.present(alertController, animated: true, completion: nil)
        
        
//        let rootviewcontroller: UIWindow = ((UIApplication.shared.delegate?.window)!)!
//        rootviewcontroller.rootViewController = self.storyboard?.instantiateViewController(withIdentifier: "SettingsViewController")
//        let mainwindow = (UIApplication.shared.delegate?.window!)!
//        mainwindow.backgroundColor = UIColor(hue: 0.6477, saturation: 0.6314, brightness: 0.6077, alpha: 0.8)
//        UIView.transition(with: mainwindow, duration: 0.55001, options: .transitionFlipFromLeft, animations: { () -> Void in
//        }) { (finished) -> Void in
//        }
        
        
    }
    
}
