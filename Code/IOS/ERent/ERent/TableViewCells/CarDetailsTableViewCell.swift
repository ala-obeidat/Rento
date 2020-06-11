//
//  CarDetailsTableViewCell.swift
//  ERent
//
//  Created by Rexxer on 4/29/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import Kingfisher

class CarDetailsTableViewCell: UITableViewCell {

    @IBOutlet weak var carImage: UIImageView!
    @IBOutlet weak var carModelName: UILabel!
    @IBOutlet weak var carType: UILabel!
    @IBOutlet weak var carModel: UILabel!
    @IBOutlet weak var carYear: UILabel!
    @IBOutlet weak var carColorSample: UIView!
    @IBOutlet weak var carColorName: UILabel!
    @IBOutlet weak var locationIcon: UIImageView!
    @IBOutlet weak var carAddress: UILabel!
    @IBOutlet weak var bookNowButton: UIButton!
    @IBOutlet weak var callButton: UIButton!
    @IBOutlet weak var favoriteButton: UIButton!
    
    var car : CarModel!
    var superVC : UIViewController? = nil
    
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
        
        carColorSample.layer.cornerRadius = carColorSample.frame.size.height/2
        carColorSample.clipsToBounds = true
        
        
        bookNowButton.layer.cornerRadius = bookNowButton.frame.size.height/2
        bookNowButton.clipsToBounds = true
        
        callButton.layer.cornerRadius = callButton.frame.size.height/2
        callButton.clipsToBounds = true
        
    }
    
    func populateWithCarModel(car : CarModel) -> Void {
        self.car = car
        
        let url = URL(string: car.image!)
        carImage.kf.setImage(with: url)
        
        carModelName.text = car.modelName
        carType.text = car.type
        carModel.text = car.model
        carYear.text = car.year
        ///need proper implementation when understanding color return from api
        
        if #available(iOS 11.0, *) {
            carColorSample.backgroundColor = UIColor(named: car.color!)
        } else {
            // Fallback on earlier versions
        }
        
        carColorName.text = car.colorName
        //locationIcon need implementation
        
        
        var myMutableString = NSMutableAttributedString()
 
        myMutableString = NSMutableAttributedString(string: "Adress :\(car.adress ?? " "))")
        
     
            
            myMutableString.setAttributes([NSAttributedStringKey.foregroundColor : UIColor(red: 0.106, green: 0.408, blue: 0.623, alpha: 1.0)  ], range: NSRange(location:0,length:8))
            
        carAddress.attributedText = myMutableString
        
        
        if car.isFavorite && car.isFavorite {
            favoriteButton.setImage(UIImage(named: "ic_heart_empty"), for: .normal)
            
        }else{
            favoriteButton.setImage(UIImage(named: "ic_heart_fill"), for: .normal)
            
        }
        
    
        
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

    @IBAction func bookNowAction(_ sender: UIButton) {
//        let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "BookNowViewController") as! BookNowViewController
//        vc.car = car
//        if superVC != nil {
//            superVC?.navigationController?.pushViewController(vc, animated: true)
//        }
        
        
    }
    @IBAction func callAction(_ sender: UIButton) {
        if car.mobile != nil {
            
        
        guard let number = URL(string: "tel://" + car.mobile!) else { return }
        UIApplication.shared.open(number)
        } else{
            print("car mobile is null")
        }
    }
    
    @IBAction func favoriteAction(_ sender: UIButton) {
        
        ///call favorite api
        if  car.isFavorite {
            favoriteButton.setImage(UIImage(named: "ic_heart_empty"), for: .normal)
            
        }else{
            favoriteButton.setImage(UIImage(named: "ic_heart_fill"), for: .normal)
            
        }
        car.isFavorite = !car.isFavorite
        
        
    }
    
}
