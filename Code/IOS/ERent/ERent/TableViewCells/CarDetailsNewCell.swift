//
//  CarDetailsNewCell.swift
//  ERent
//
//  Created by Rexxer on 6/1/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import Kingfisher

class CarDetailsNewCell: UITableViewCell {

    @IBOutlet weak var carImageView: UIImageView!
    @IBOutlet weak var modelNameLabel: UILabel!
    @IBOutlet weak var typeLabel: UILabel!
    @IBOutlet weak var yearLabel: UILabel!
    @IBOutlet weak var model: UILabel!
    @IBOutlet weak var colorLabel: UILabel!
    @IBOutlet weak var providerLabel: UILabel!
    @IBOutlet weak var dailyCostLabel: UILabel!
    var car : CarModel!
    var superVC : UIViewController? = nil
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

    
    
    func populateWithCarModel(car : CarModel) -> Void {
        self.car = car
        
        let url = URL(string: car.image!)
        carImageView.kf.setImage(with: url)
        
        modelNameLabel.text = car.getTypeName() + " " + car.getSubTypeName()
        typeLabel.text = car.getTypeName()
        model.text = car.getSubTypeName()
        yearLabel.text = car.model
        
        colorLabel.text = ""
      //  colorLabel.backgroundColor = UIColor(hex: car.color ?? "ffffff")
        let colorStr =  car.color ?? "#ffffff"
        let trimmedColorStr = colorStr.trimmingCharacters(in: .whitespacesAndNewlines)
        
        colorLabel.backgroundColor =  UIColor(hexString: trimmedColorStr)
        
        let token : String = UserDefaults.standard.object(forKey: "Token") as? String ?? ""
        if token == "" {
             providerLabel.text = "--"
        }else {
           providerLabel.text = car.officeName
        }
        
        if car.dayCost == "0" {
            dailyCostLabel.text = (car.price ?? "0") + " " + "SR"
        }else {
            dailyCostLabel.text = car.dayCost + " " + "SR"
        }
        
        
    }
}
