//
//  CarRequestTableViewCell.swift
//  ERent
//
//  Created by Rexxer on 5/4/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class CarRequestTableViewCell: UITableViewCell {

    @IBOutlet weak var carImage: UIImageView!
    @IBOutlet weak var carModelName: UILabel!
    @IBOutlet weak var date: UILabel!
    @IBOutlet weak var modelYear: UILabel!
    @IBOutlet weak var time: UILabel!
    @IBOutlet weak var status: UILabel!
    @IBOutlet weak var alertButton: UIButton!
    @IBOutlet weak var closeBookingButton: UIButton!
    
    @IBOutlet weak var closeBookingIcon: UIImageView!
    var car : CarModel!
    
    var vc : RequestsViewController!
    
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
        
        closeBookingButton.layer.cornerRadius = closeBookingButton.frame.size.height/2
        closeBookingButton.clipsToBounds = true
        
        alertButton.layer.cornerRadius = alertButton.frame.size.height/2
        alertButton.clipsToBounds = true
        alertButton.isHidden = true
        

    }

    func populateWithCarModel(car : CarModel) {
        self.car = car
        let url = URL(string: car.image!)
        carImage.kf.setImage(with: url)
        carModelName.text = car.getTypeName() + " " + car.getSubTypeName()
        date.text = car.date
        modelYear.text = car.model
        var temp1 : String! // This is not optional.
        temp1 = car.time
        time.text = temp1
    
//        status.text = car.status
        
        status.text = "\(car.actionType! )"
        
        status.text = getStatusName(status: (car.actionType?.rawValue)!)
        
        if car.actionType?.rawValue == 2 {
            closeBookingButton.isHidden = false
            closeBookingIcon.isHidden = false
        }else{
            closeBookingButton.isHidden = true
            closeBookingIcon.isHidden = true
        }
        
    }
    
    
    func  getStatusName(status: Int) -> String {
    var name = ""
    switch status {
    case 0:
    name = "pending".localized
    break;
    case 1:
    name = "processing".localized
    break;
    case 2:
    name = "approved".localized
    break;
    case 3:
    name = "on_the_way".localized
    break;
    case 4:
    name = "delivered".localized
    break;
    case 5:
    name = "done".localized
    break;
    case 6:
    name = "rejected".localized
    break
    default:
        name = "pending".localized
        }
    return name
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

    @IBAction func closeBookingAction(_ sender: UIButton) {
        let closeBookingViewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "CloseBookingViewController") as! CloseBookingViewController
        
        closeBookingViewController.modalPresentationStyle = .overCurrentContext
        closeBookingViewController.modalTransitionStyle = .crossDissolve
        
        closeBookingViewController.car = car
        vc.present(closeBookingViewController, animated: true, completion: nil)
        
        
    }
    
    @IBAction func alertAction(_ sender: UIButton) {
        
    }
    
}
