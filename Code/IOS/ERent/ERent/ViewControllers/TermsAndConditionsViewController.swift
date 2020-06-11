//
//  TermsAndConditionsViewController.swift
//  ERent
//
//  Created by Zaid Khaled on 6/5/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class TermsAndConditionsViewController: UIViewController {
    @IBOutlet weak var termsConditions: UITextView!
    var str : String = ""
    override func viewDidLoad() {
        super.viewDidLoad()
        
         let imageName = "logo"
        let image = UIImage(named: imageName)
        let imageView = UIImageView(image: image!)
        
        
        imageView.image = imageView.image!.withRenderingMode(.alwaysTemplate)
        imageView.tintColor = UIColor.white
        
        imageView.contentMode = UIViewContentMode(rawValue: 1)!
        imageView.frame = CGRect(x: 0, y: 0, width: 60, height: 30)
        navigationItem.titleView = imageView

        termsConditions.text = str
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
  

}
