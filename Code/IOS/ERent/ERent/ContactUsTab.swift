//
//  ContactUsTab.swift
//  ERent
//
//  Created by Zaid najjar on 7/4/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import XLPagerTabStrip
class ContactUsTab: UIViewController, IndicatorInfoProvider {

    @IBOutlet weak var emailText: UITextView!
    @IBOutlet weak var touchBtn: UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()

        
        touchBtn.layer.cornerRadius = touchBtn.frame.height/2
        touchBtn.clipsToBounds = true
        // Do any additional setup after loading the view.
        
        emailText.isEditable = false;
        emailText.dataDetectorTypes = .all
        
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    func indicatorInfo(for pagerTabStripController: PagerTabStripViewController) -> IndicatorInfo {
        return IndicatorInfo(title: "contact_us".localized)
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
