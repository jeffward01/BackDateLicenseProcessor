using System;
using System.Timers;
using NLog;
using NLog.Internal;
using Topshelf;
using Topshelf.Hosts;
using UMPG.USL.BackDateProcessor.Business;
using UMPG.USL.BackDateProcessor.Business.Logging;
using UMPG.USL.BackDateProcessor.Business.Managers;
using UMPG.USL.BackDateProcessor.Business.Services;
using UMPG.USL.BackDateProcessor.Data.Repositories;

namespace UMPG.USL.BackDateProcessor.Cmd.Application
{
    internal class LicenseSnapshotBackDateService : ServiceControl
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IViewManager _viewManager;
       // private IServiceManager _serviceManager;

        public LicenseSnapshotBackDateService()
        {

            _viewManager = new ViewManager(new LicenseManager(new LicenseRepository()));
            //Instantiate timers
            
            //_serviceManager = new ServiceManager(new DataHarmonizationLogManager(), new DataHarmonizationQueueService(new DataHarmonizationQueueRepository(), new DataHarmonizationLogManager(), new SnapshotLicenseRepository()), new DataProcessorService(new TimeSpanUtil()), new DataHarmonizationManager(new DataHarmonizationLogManager(), new LicenseRepository(), new DataHarmonizationQueueService(new DataHarmonizationQueueRepository(), new DataHarmonizationLogManager(), new SnapshotLicenseRepository()), new LicenseProductService(new DataHarmonizationLogManager()), new SnapshotManager(new SnapshotLicenseManager(new SnapshotLicenseRepository(), new SnapshotComposerOriginalPublisherAdminKnownAsRepository(), new SnapshotComposerRepository(), new SnapshotComposerAffiliationRepository(), new SnapshotComposerAffiliationBaseRepository(), new Snapshot_ComposerKnownAsRepository(), new SnapshotComposerOriginalPublisherAffiliationBaseRepository(), new Snapshot_ComposerOriginalPublisherAffiliationRepository(), new Snapshot_ComposerOriginalPublisherRepository(), new Snapshot_ComposerOriginalPublisherKnownAsRepository(), new SnapshotSampleAquisitionLocationCodeRepository(), new SnapshotSampleLocalClientCopyrightRepository(), new SnapshotComposerOriginalPublisherAdministratorRepository(), new SnapshotComposerOriginalPublisherAdminAffiliationRepository(), new SnapshotComposerOriginalPublisherAdminAffiliationBaseRepository(), new SnapshotOriginalPubAffiliationBaseRepository(), new SnapshotAffiliationBaseRepository(), new SnapshotOriginalPublisherAffiliationRepository(), new SnapshotAdminAffiliationBaseRepository(), new SnapshotAdminAffiliationRepository(), new SnapshotAdminKnownAsRepository(), new SnapshotAdministratorRepository(), new LicenseProductConfigurationRepository(), new SnapshotAquisitionLocationCodeRepository(), new SnapshotRecsCopyrightRespository(), new SnapshotSampleRepository(), new SnapshotOriginalPublisherRepository(), new SnapshotKnownAsRepository(), new SnapshotAffiliationRepository(), new SnapshotWorksWriterRepository(), new SnapshotWorkTrackRepository(), new SnapshotLicenseProductRepository(), new SnapshotWorksRecordingRepository(), new SnapshotRecsConfigurationRepository(), new SnapshotProductHeaderRepository(), new SnapshotConfigurationRepository(), new SnapshotArtistRecsRepository(), new SnapshotLabelRepository(), new SnapshotLabelGroupRepository(), new SnapshotLocalClientCopyrightRepository()), new SnapshotLicenseProductManager(new SnapshotLicenseRepository(), new SnapshotComposerOriginalPublisherAdminKnownAsRepository(), new SnapshotComposerRepository(), new SnapshotComposerAffiliationRepository(), new SnapshotComposerAffiliationBaseRepository(), new Snapshot_ComposerKnownAsRepository(), new SnapshotComposerOriginalPublisherAffiliationBaseRepository(), new Snapshot_ComposerOriginalPublisherAffiliationRepository(), new Snapshot_ComposerOriginalPublisherRepository(), new Snapshot_ComposerOriginalPublisherKnownAsRepository(), new SnapshotSampleRepository(), new SnapshotSampleAquisitionLocationCodeRepository(), new SnapshotSampleLocalClientCopyrightRepository(), new SnapshotComposerOriginalPublisherAdministratorRepository(), new SnapshotComposerOriginalPublisherAdminAffiliationRepository(), new SnapshotComposerOriginalPublisherAdminAffiliationBaseRepository(), new SnapshotOriginalPubAffiliationBaseRepository(), new SnapshotOriginalPublisherAffiliationRepository(), new SnapshotAffiliationBaseRepository(), new SnapshotAdminKnownAsRepository(), new SnapshotAdminAffiliationBaseRepository(), new SnapshotAdminAffiliationRepository(), new SnapshotAdministratorRepository(), new SnapshotAquisitionLocationCodeRepository(), new SnapshotRecsCopyrightRespository(), new SnapshotOriginalPublisherRepository(), new SnapshotKnownAsRepository(), new SnapshotAffiliationRepository(), new SnapshotWorksWriterRepository(), new SnapshotWorkTrackRepository(), new SnapshotLicenseProductRepository(), new SnapshotWorksRecordingRepository(), new SnapshotRecsConfigurationRepository(), new SnapshotProductHeaderRepository(), new SnapshotConfigurationRepository(), new SnapshotArtistRecsRepository(), new SnapshotLabelRepository(), new SnapshotLabelGroupRepository(), new SnapshotLocalClientCopyrightRepository()))), new DataHarmonizationQueueRepository(), new SqlConnectionManager());
        }

        private void ActivateService(object sender, System.Timers.ElapsedEventArgs e)
        {
   
        }


        #region ServiceControl Members

        public bool Start(HostControl hostControl)
        {
            Logger.Debug("***LicenseBackDateProcessor service starts.***");
            Console.WriteLine("Is running as console: " + IsRunningAsConsole(hostControl));
            try
            {
                _viewManager.DisplayWelcomeMessage();


            }
            catch (Exception failPollingException)
            {
                Logger.Error(failPollingException, "*****************Cannot start LicenseBackDateProcessor service.*****************");

                return false;
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Logger.Info("*************LicenseBackDateProcessor service terminated.**************");
            return true;
        }

        private static bool IsRunningAsConsole(HostControl control)
        {
            return control is ConsoleRunHost;
        }

        #endregion ServiceControl Members
    }
}