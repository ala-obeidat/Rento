//
//  CarOffersTableViewCell.swift
//  ERent
//
//  Created by Rexxer on 5/6/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class CarOffersTableViewCell: UITableViewCell {


    @IBOutlet weak var officeLabel: UILabel!
    
    @IBOutlet weak var carModelName: UILabel!
    @IBOutlet weak var carImage: UIImageView!
    @IBOutlet weak var date: UILabel!
    @IBOutlet weak var carPrice: UILabel!
    @IBOutlet weak var carModel: UILabel!
    
    var car : CarModel!
    
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

    func populateWithCarModel(car : CarModel) {
        self.car = car
        let url = URL(string: car.image!)
        carImage.kf.setImage(with: url)
        carModelName.text = car.getTypeName() + " " + car.getSubTypeName()
        date.text = car.date
        
        
        carPrice.text = car.cost + " SR"
        carModel.text = car.model
        officeLabel.text = car.providerName
        
    }
}
