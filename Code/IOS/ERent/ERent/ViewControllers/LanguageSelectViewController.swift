//
//  LanguageSelectViewController.swift
//  ERent
//
//  Created by Rexxer on 5/25/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class LanguageSelectViewController: UIViewController {

    @IBOutlet weak var titleText: UILabel!
    
    @IBOutlet weak var arabicButton: UIButton!
    @IBOutlet weak var englishButton: UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()

        
        arabicButton.layer.cornerRadius = 5.0
        arabicButton.clipsToBounds = true
        
        
        englishButton.layer.cornerRadius = 5.0
        englishButton.clipsToBounds = true
        
        titleText.text = "Please seelct your preferred language".localized
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    

    @IBAction func englishAction(_ sender: UIButton) {
        L102Language.setAppleLAnguageTo(lang: "en")
        let defaults = UserDefaults.standard
        defaults.set("en", forKey: "language")
        goToLoginScreen()
    }
    
    
    @IBAction func arabicAction(_ sender: UIButton) {
        L102Language.setAppleLAnguageTo(lang: "ar")
        let defaults = UserDefaults.standard
        defaults.set("ar", forKey: "language")
        goToLoginScreen()
        
    }
    
    func goToLoginScreen() {
//        L012Localizer.DoTheSwizzling()
        
        let viewController:UIViewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "HomeTabbarController") as UIViewController
        //        self.present(viewController, animated: true, completion: nil)
        self.navigationController?.pushViewController(viewController, animated: true)
    }
}
