//
//  VerificationSuccessViewController.swift
//  ERent
//
//  Created by Rexxer on 5/13/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class VerificationSuccessViewController: UIViewController {
    @IBOutlet weak var signInButton: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        
        signInButton.layer.cornerRadius = 0.5 * signInButton.bounds.size.height
        signInButton.clipsToBounds = true
        
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func signInAction(_ sender: UIButton) {
        
//        let viewController:UIViewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "LoginViewController") as UIViewController
//        self.navigationController?.pushViewController(viewController, animated: true)

        
        let viewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "HomeTabbarController") as UIViewController
        self.present(viewController, animated: true, completion: nil)
    }
    

}
