import { Routes } from "@angular/router";
 import{InwardUploadComponent} from './inward-upload/InwardUpload.component'
 
export const uploadRoutes: Routes = [
  {
    path: "",
    children: [
      // {
        {
          path: "InwardUpload",
          component: InwardUploadComponent
        }

      
    ]
  }
];
