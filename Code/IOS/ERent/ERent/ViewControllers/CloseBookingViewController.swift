//
//  CloseBookingViewController.swift
//  ERent
//
//  Created by Rexxer on 6/3/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import Cosmos
import SVProgressHUD
class CloseBookingViewController: UIViewController {
//accept 150
//decline 250
    @IBOutlet weak var containerViewHeightConstraint: NSLayoutConstraint!
    
    
    @IBOutlet weak var container: UIView!
    var car : CarModel!
    
    @IBOutlet weak var segmentControl: UISegmentedControl!
    
    @IBOutlet weak var commentTextField: UITextField!
    
    @IBOutlet weak var cancelButton: UIButton!
    @IBOutlet weak var doneButton: UIButton!
    
    
    @IBOutlet weak var ratingContainerView: UIView!
    
    @IBOutlet weak var ratingView: CosmosView!
    var flag : Int = 0
    
    var rate : Int = 0
    override func viewDidLoad() {
        super.viewDidLoad()
//        ratingContainerView.isHidden = true
        view.isOpaque = false
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    @IBAction func segmentValueChanged(_ sender: UISegmentedControl) {
        if sender.selectedSegmentIndex == 0 {
            
//            ratingContainerView.isHidden = false
//            commentTextField.isHidden = true
            containerViewHeightConstraint.constant = 150
            flag = 2
        }else {
//            ratingContainerView.isHidden = true
//            commentTextField.isHidden = false
            containerViewHeightConstraint.constant = 220
            flag = 3
        }
      
        UIView.animate(withDuration: 0.2, animations: {
            self.container.layoutIfNeeded()
        }, completion: {
            (value: Bool) in
            if self.flag == 2 {
                self.commentTextField.isHidden = true
            }else {
                self.commentTextField.isHidden = false
            }
        })
    }
    
    @IBAction func cancelAction(_ sender: UIButton) {
        self.dismiss(animated: true) {
            
        }
    }
    
    @IBAction func doneAction(_ sender: UIButton) {
        if (segmentControl.selectedSegmentIndex == 1) {
            if (commentTextField.text == nil || commentTextField.text == "") {
                self.displayAlert(message: "Enter comment first!".localized)
                return
            }
        }
        rate = Int(ratingView.rating)
        
        let request = CloseBookingRequest(checkoutId: Int(car.id)!, star: rate, comment: commentTextField.text!, flag: flag)
        
        SVProgressHUD.show()
        
        APIClient.execute(request: request) { [weak self] result in
            SVProgressHUD.dismiss()
            switch result {
            case .success(_):
                self?.dismiss(animated: true) {
                    
                }
                break
            case .failure(let error):
                self?.displayAlert(message: error)
            }
        }
        
    }
    
    
}
