//
//  FavoriteViewController.swift
//  ERent
//
//  Created by Rexxer on 5/8/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import XLPagerTabStrip
class FavoriteViewController: UIViewController , UITableViewDelegate , UITableViewDataSource , IndicatorInfoProvider{
//    @IBOutlet weak var topViewController: UIView!
//    
//    @IBOutlet weak var titleLabel: UILabel!
//    @IBOutlet weak var titleIcon: UIImageView!
    
    @IBOutlet weak var tableView: UITableView!
    
    var items = [CarModel]()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
        
//        let imageName = "logo"
//        let image = UIImage(named: imageName)
//        let imageView = UIImageView(image: image!)
//        imageView.contentMode = UIViewContentMode(rawValue: 1)!
//        imageView.frame = CGRect(x: 0, y: 0, width: 60, height: 30)
//        navigationItem.titleView = imageView
//         
        tableView.delegate = self
        tableView.dataSource = self
  
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        let appDelegate: AppDelegate? = UIApplication.shared.delegate as? AppDelegate
        
        
        items = (appDelegate?.getFavorites())!
        
        tableView.reloadData()
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

    //home ,requests,offers,messages,profile
    //profile profile data , image , name , edit profile
    //history , favorite , in the profile
    //in profile in nav settings , change lang and push
    //logout in the bottom of any screen
    
    
    //login, signup today
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 180.0
    }
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return items.count
        
    }
    
    
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell{
        
        let cell  = tableView.dequeueReusableCell(withIdentifier: "CarDetailsNewCell") as! CarDetailsNewCell
        cell.populateWithCarModel(car: items[indexPath.row])
        cell.superVC = self
        
        return cell
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        let token : String = UserDefaults.standard.object(forKey: "IS_REGISTERED") as? String ?? ""
        if token == "" {
            let alertController = UIAlertController(title: "alert".localized, message: "must_login".localized, preferredStyle: .alert)
            
            // Create the actions
            let okAction = UIAlertAction(title: "login".localized, style: UIAlertActionStyle.default) {
                UIAlertAction in
                let storyboard: UIStoryboard = UIStoryboard(name: "Main", bundle: nil)
                let vc: UIViewController = storyboard.instantiateViewController(withIdentifier: "LoginViewController") as! LoginViewController
                //  self.present(vc, animated: true, completion: nil)
                self.navigationController?.pushViewController(vc, animated: true)
            }
            let cancelAction = UIAlertAction(title: "cancel".localized, style: UIAlertActionStyle.cancel) {
                UIAlertAction in
                NSLog("Cancel Pressed")
            }
            
            // Add the actions
            alertController.addAction(okAction)
            alertController.addAction(cancelAction)
            
            // Present the controller
            self.present(alertController, animated: true, completion: nil)
            
            
        }else {
            let vc = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "BookingViewController") as! BookingViewController
            vc.car = items[indexPath.row]
            navigationController?.pushViewController(vc, animated: true)
        }
     
    }
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    
    
    func indicatorInfo(for pagerTabStripController: PagerTabStripViewController) -> IndicatorInfo {
        return IndicatorInfo(title: "favourites".localized)
    }
}
