//
//  RequestsViewController.swift
//  ERent
//
//  Created by Rexxer on 5/4/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import SVProgressHUD
class RequestsViewController: UIViewController ,UITableViewDelegate , UITableViewDataSource{
    @IBOutlet weak var tableView: UITableView!
    
    @IBOutlet weak var topView: UIView!
    
    @IBOutlet weak var titleLabel: UILabel!
    
    @IBOutlet weak var titleIcon: UIImageView!
    
    var items = [CarModel]()
    
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

        
        tableView.contentInset = UIEdgeInsets(top: 0, left: 0, bottom: 50, right: 0)
        
        tableView.delegate = self
        tableView.dataSource = self
        
        
        let token : String = UserDefaults.standard.object(forKey: "IS_REGISTERED") as? String ?? ""
        if token == "" {
    
        }else {
              getCars()
        }
        
    }
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        let token : String = UserDefaults.standard.object(forKey: "Token") as? String ?? ""
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
            getCars()
        }
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
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 150
    }
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return items.count
       
    }
    
    
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell  = tableView.dequeueReusableCell(withIdentifier: "CarRequestTableViewCell") as! CarRequestTableViewCell
        cell.populateWithCarModel(car: items[indexPath.row])
        cell.vc = self
        return cell
        
    }
    
    
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    
    func getCars() {
        SVProgressHUD.show()
        
//        let request = CarListService(serviceType: .list, cityId: 1)
        let request = ListRequestService(isHistory: false)
        
        APIClient.execute(request: request) { [weak self] result in
            SVProgressHUD.dismiss()
            switch result {
            case .success(let cars):
                self?.items = cars
                self?.tableView.reloadData()
            case .failure(let error):
                self?.displayAlert(message: error)
            }
        }
    }
}
