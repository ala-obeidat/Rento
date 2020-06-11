//
//  ProfileViewController.swift
//  ERent
//
//  Created by Rexxer on 5/15/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class ProfileViewController: UIViewController {

    @IBOutlet weak var containerView: UIView!
    
    @IBOutlet weak var welcomeLabel: UILabel!
    
    @IBOutlet weak var userNameLabel: UILabel!
    
    @IBOutlet weak var imageView: UIImageView!
    
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
         let pageAdapterVC = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "PagerAdapter")
        let newView = pageAdapterVC.view
        
        
        addChildViewController(pageAdapterVC);
        containerView.addSubview(newView!)
        newView?.translatesAutoresizingMaskIntoConstraints = false
        pageAdapterVC.didMove(toParentViewController: self)
        
        let topConst = NSLayoutConstraint(item: newView!, attribute: NSLayoutAttribute.top, relatedBy: NSLayoutRelation.equal, toItem: containerView, attribute: NSLayoutAttribute.top, multiplier: 1, constant: 0)
        let botConst = NSLayoutConstraint(item: newView!, attribute: NSLayoutAttribute.bottom, relatedBy: NSLayoutRelation.equal, toItem: containerView, attribute: NSLayoutAttribute.bottom, multiplier: 1, constant: 0)
        let leftConst = NSLayoutConstraint(item: newView!, attribute: NSLayoutAttribute.leading, relatedBy: NSLayoutRelation.equal, toItem: containerView, attribute: NSLayoutAttribute.leading, multiplier: 1, constant: 0)
        let rigthConst = NSLayoutConstraint(item: newView!, attribute: NSLayoutAttribute.trailing, relatedBy: NSLayoutRelation.equal, toItem: containerView, attribute: NSLayoutAttribute.trailing, multiplier: 1, constant: 0)

        NSLayoutConstraint.activate([topConst,botConst,leftConst,rigthConst])

        
        let userName  = UserDefaults.standard.object(forKey: "Username") as? String ?? ""
        userNameLabel.text = userName
        
    // also give it frame
        let singleTap = UITapGestureRecognizer(target: self, action: #selector(ProfileViewController.tapDetected))
        imageView.isUserInteractionEnabled = true
        imageView.addGestureRecognizer(singleTap)
        
    }
    
    //Action
    @objc func tapDetected() {
        print("Imageview Clicked")
        let text = "Download E-Rent app for the best car booking deals1 \n\n itms://itunes.apple.com/de/app/x-gift/id1396903742?mt=8&uo=4"
        let textShare = [ text ]
        let activityViewController = UIActivityViewController(activityItems: textShare , applicationActivities: nil)
        activityViewController.popoverPresentationController?.sourceView = self.view
        self.present(activityViewController, animated: true, completion: nil)
    }

    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        self.navigationController?.isNavigationBarHidden = true
        
    }
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(animated)
        self.navigationController?.isNavigationBarHidden = false
        
    }
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */
    
    
    @IBAction func openSettings(_ sender: UIButton) {
        
        let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "SettingsViewController") as! SettingsViewController
        
        navigationController?.pushViewController(vc, animated: true)
        
    }
    
}
