using UMPG.USL.BackDateProcessor.Data.Repositories;
using NLog;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class SnapshotLicenseManager : ISnapshotLicenseManager
    {
        private readonly ISnapshotLicenseRepository _snapshotLicenseRepository;
        private readonly ISnapshotLicenseProductRepository _snapshotLicenseProductRepository;
        private readonly ISnapshotWorksRecordingRepository _snapshotWorksRecordingRepository;
        private readonly ISnapshotRecsConfigurationRepository _snapshotRecsConfigurationRepository;
        private readonly ISnapshotProductHeaderRepository _snapshotProductHeaderRepository;
        private readonly ISnapshotConfigurationRepository _snapshotConfigurationRepository;
        private readonly ISnapshotArtistRecsRepository _snapshotArtistRecsRepository;
        private readonly ISnapshotLabelRepository _snapshotLabelRepository;
        private readonly ISnapshotLabelGroupRepository _snapshotLabelGroupRepository;

        //   private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ISnapshotWorkTrackRepository _snapshotWorkTrackRepository;

        private readonly ISnapshotWorksWriterRepository _snapshotWorksWriterRepository;
        private readonly ISnapshotAffiliationRepository _snapshotAffiliationRepository;
        private readonly ISnapshotKnownAsRepository _snapshotKnownAsRepository;
        private readonly ISnapshotOriginalPublisherRepository _snapshotOriginalPublisherRepository;
        private readonly ISnapshotRecsCopyrightRespository _snapshotRecsCopyrightRespository;
        private readonly ISnapshotSampleRepository _snapshotSampleRepository;
        private readonly ISnapshotLocalClientCopyrightRepository _snapshotLocalClientCopyrightRepository;
        private readonly ISnapshotAquisitionLocationCodeRepository _aquisitionLocationCodeRepository;
        private readonly ILicenseProductConfigurationRepository _licenseProductConfigurationRepository;
        private readonly ISnapshotAdministratorRepository _snapshotAdministratorRepository;
        private readonly ISnapshotAdminKnownAsRepository _adminKnownAsRepository;
        private readonly ISnapshotAdminAffiliationRepository _adminAffiliationRepository;
        private readonly ISnapshotAdminAffiliationBaseRepository _adminAffiliationBaseRepository;
        private readonly ISnapshotOriginalPublisherAffiliationRepository _originalPublisherAffiliationRepository;
        private readonly ISnapshotOriginalPubAffiliationBaseRepository _originalPubAffiliationBaseRepository;
        private readonly ISnapshotAffiliationBaseRepository _affiliationBaseRepository;
        private readonly ISnapshotComposerRepository _snapshotComposerRepository;
        private readonly ISnapshotComposerAffiliationRepository _composerAffiliationRepository;
        private readonly ISnapshotComposerAffiliationBaseRepository _composerAffiliationBaseRepository;
        private readonly ISnapshot_ComposerKnownAsRepository _composerKnownAsRepository;

        private readonly ISnapshotComposerOriginalPublisherAffiliationBaseRepository
            _composerOriginalPublisherAffiliationBaseRepository;

        private readonly ISnapshot_ComposerOriginalPublisherAffiliationRepository
            _composerOriginalPublisherAffiliationRepository;

        private readonly ISnapshot_ComposerOriginalPublisherRepository _composerOriginalPublisherRepository;

        private readonly ISnapshot_ComposerOriginalPublisherKnownAsRepository
            _composerOriginalPublisherKnownAsRepository;

        private readonly ISnapshotSampleAquisitionLocationCodeRepository _snapshotSampleAquisitionLocationCodeRepository;
        private readonly ISnapshotSampleLocalClientCopyrightRepository _snapshotSampleLocalClientCopyrightRepository;

        private readonly ISnapshotComposerOriginalPublisherAdministratorRepository
            _composerOriginalPublisherAdministratorRepository;

        private readonly ISnapshotComposerOriginalPublisherAdminAffiliationRepository
            _composerOriginalPublisherAdminAffiliation;

        private readonly ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository
            _composerOriginalPublisherAdminAffiliationBaseRepository;

        private readonly ISnapshotComposerOriginalPublisherAdminKnownAsRepository
            _composerOriginalPublisherAdminKnownAsRepository;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SnapshotLicenseManager(ISnapshotLicenseRepository snapshotLicenseRepository,

            ISnapshotComposerOriginalPublisherAdminKnownAsRepository composerOriginalPublisherAdminKnownAsRepository,
            ISnapshotComposerRepository snapshotComposerRepository,
            ISnapshotComposerAffiliationRepository composerAffiliationRepository,
            ISnapshotComposerAffiliationBaseRepository composerAffiliationBaseRepository,
            ISnapshot_ComposerKnownAsRepository composerKnownAsRepository,
            ISnapshotComposerOriginalPublisherAffiliationBaseRepository
                composerOriginalPublisherAffiliationBaseRepository,
            ISnapshot_ComposerOriginalPublisherAffiliationRepository composerOriginalPublisherAffiliationRepository,
            ISnapshot_ComposerOriginalPublisherRepository composerOriginalPublisherRepository,
            ISnapshot_ComposerOriginalPublisherKnownAsRepository composerOriginalPublisherKnownAsRepository,

            ISnapshotSampleAquisitionLocationCodeRepository snapshotSampleAquisitionLocationCodeRepository,
            ISnapshotSampleLocalClientCopyrightRepository snapshotSampleLocalClientCopyrightRepository,
            ISnapshotComposerOriginalPublisherAdministratorRepository composerOriginalPublisherAdministratorRepository,
            ISnapshotComposerOriginalPublisherAdminAffiliationRepository
                composerOriginalPublisherAdminAffiliationRepository,
            ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository
                composerOriginalPublisherAdminAffiliationBaseRepository,

            //      ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ISnapshotOriginalPubAffiliationBaseRepository snapshotOriginalPubAffiliationBaseRepository,
            ISnapshotAffiliationBaseRepository affiliationBaseRepository,
            ISnapshotOriginalPublisherAffiliationRepository snapshotOriginalPublisherAffiliationRepository,
            ISnapshotAdminAffiliationBaseRepository snapshotAdminAffiliationBaseRepository,
            ISnapshotAdminAffiliationRepository snapshotAdminAffiliationRepository,
            ISnapshotAdminKnownAsRepository snapshotAdminKnownAsRepository,
            ISnapshotAdministratorRepository snapshotAdministratorRepository,
            ILicenseProductConfigurationRepository licenseProductConfigurationRepository,
            ISnapshotAquisitionLocationCodeRepository aquisitionLocationCodeRepository,
            ISnapshotRecsCopyrightRespository snapshotRecsCopyrightRespository,
            ISnapshotSampleRepository snapshotSampleRepository,
            ISnapshotOriginalPublisherRepository snapshotOriginalPublisherRepository,
            ISnapshotKnownAsRepository snapshotKnownAsRepository,
            ISnapshotAffiliationRepository snapshotAffiliationRepository,
            ISnapshotWorksWriterRepository snapshotWorksWriterRepository,
            ISnapshotWorkTrackRepository snapshotWorkTrackRepository,
            ISnapshotLicenseProductRepository snapshotLicenseProductRepository,

            ISnapshotWorksRecordingRepository snapshotWorksRecordingRepository,
            ISnapshotRecsConfigurationRepository snapshotRecsConfigurationRepository,
            ISnapshotProductHeaderRepository snapshotProductHeaderRepository,
            ISnapshotConfigurationRepository snapshotConfigurationRepository,
            ISnapshotArtistRecsRepository snapshotArtistRecsRepository, ISnapshotLabelRepository snapshotLabelRepository,
            ISnapshotLabelGroupRepository snapshotLabelGroupRepository,
            ISnapshotLocalClientCopyrightRepository snapshotLocalClientCopyrightRepository
        )
        {
            _composerOriginalPublisherAdminKnownAsRepository = composerOriginalPublisherAdminKnownAsRepository;
            _composerOriginalPublisherAdminAffiliationBaseRepository =
                composerOriginalPublisherAdminAffiliationBaseRepository;
            _composerOriginalPublisherAdminAffiliation = composerOriginalPublisherAdminAffiliationRepository;
            _composerOriginalPublisherAdministratorRepository = composerOriginalPublisherAdministratorRepository;

            _snapshotComposerRepository = snapshotComposerRepository;
            _composerAffiliationRepository = composerAffiliationRepository;
            _composerAffiliationBaseRepository = composerAffiliationBaseRepository;
            _composerKnownAsRepository = composerKnownAsRepository;
            _composerOriginalPublisherAffiliationBaseRepository = composerOriginalPublisherAffiliationBaseRepository;
            _composerOriginalPublisherAffiliationRepository = composerOriginalPublisherAffiliationRepository;
            _composerOriginalPublisherRepository = composerOriginalPublisherRepository;
            _composerOriginalPublisherKnownAsRepository = composerOriginalPublisherKnownAsRepository;
            _snapshotSampleAquisitionLocationCodeRepository = snapshotSampleAquisitionLocationCodeRepository;
            _snapshotSampleLocalClientCopyrightRepository = snapshotSampleLocalClientCopyrightRepository;
            //    _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _affiliationBaseRepository = affiliationBaseRepository;
            _originalPublisherAffiliationRepository = snapshotOriginalPublisherAffiliationRepository;
            _originalPubAffiliationBaseRepository = snapshotOriginalPubAffiliationBaseRepository;
            _adminAffiliationBaseRepository = snapshotAdminAffiliationBaseRepository;
            _adminAffiliationRepository = snapshotAdminAffiliationRepository;
            _snapshotAdministratorRepository = snapshotAdministratorRepository;
            _aquisitionLocationCodeRepository = aquisitionLocationCodeRepository;
            _snapshotLocalClientCopyrightRepository = snapshotLocalClientCopyrightRepository;
            _snapshotSampleRepository = snapshotSampleRepository;
            _snapshotRecsCopyrightRespository = snapshotRecsCopyrightRespository;
            _snapshotOriginalPublisherRepository = snapshotOriginalPublisherRepository;
            _snapshotKnownAsRepository = snapshotKnownAsRepository;
            _snapshotAffiliationRepository = snapshotAffiliationRepository;
            _snapshotWorksWriterRepository = snapshotWorksWriterRepository;
            _snapshotWorkTrackRepository = snapshotWorkTrackRepository;
            _licenseProductConfigurationRepository = licenseProductConfigurationRepository;

            _snapshotLabelGroupRepository = snapshotLabelGroupRepository;
            _snapshotLabelRepository = snapshotLabelRepository;
            _snapshotArtistRecsRepository = snapshotArtistRecsRepository;
            _snapshotConfigurationRepository = snapshotConfigurationRepository;
            _snapshotProductHeaderRepository = snapshotProductHeaderRepository;
            _snapshotRecsConfigurationRepository = snapshotRecsConfigurationRepository;
            _snapshotWorksRecordingRepository = snapshotWorksRecordingRepository;
            _snapshotLicenseProductRepository = snapshotLicenseProductRepository;
            _snapshotLicenseRepository = snapshotLicenseRepository;
            _adminKnownAsRepository = snapshotAdminKnownAsRepository;
        }

        public bool DoesSnapshotExists(int licenseId)
        {
            return _snapshotLicenseRepository.DoesLicenseSnapshotExist(licenseId);
        }

        public Snapshot_License SaveSnapshotLicense(Snapshot_License snapshotLicense)
        {
            return _snapshotLicenseRepository.SaveSnapshotLicense(snapshotLicense);
        }

        public bool MarkSnapshotLicenseComplete(Snapshot_License snapshotLicense)
        {
            snapshotLicense.SnapshotComplete = true;
            var result = _snapshotLicenseRepository.EditSnapshotLicense(snapshotLicense);
            return result;
        }

        public Snapshot_License GetSnapshotLicenseBySnapshotLicenseId(int snapshotLicenseId)
        {
            var licenseInformation = _snapshotLicenseRepository.GetLicenseSnapShotById(snapshotLicenseId);

            var licenseProducts =
                _snapshotLicenseProductRepository.GetAllLicenseProductsForLicenseId(licenseInformation.CloneLicenseId);

            foreach (var licenseProduct in licenseProducts)
            {
                var configs =
                                   _snapshotRecsConfigurationRepository
                                       .GetAllRecsConfigurationsRecordingsForProductHeaderId(
                                           licenseProduct.ProductHeader.SnapshotProductHeaderId);
                foreach (var config in configs)
                {
                    if (config.LicenseProductId != null && config.LicenseProductConfigurationId != null)
                    {
                        var lpc = _licenseProductConfigurationRepository
                                  .GetLicenseProductConfigurationByProductIdAndLicenseProductConfigurationId((int)config.LicenseProductId, (int)config.LicenseProductConfigurationId);
                        config.LicenseProductConfiguration = lpc;
                    }
                }

                licenseProduct.ProductHeader.Configurations = configs;
            }

            licenseInformation.LicenseProducts = licenseProducts;
            return licenseInformation;
        }

        //Jeffs new method.  V-1.0

        public bool DeleteLicenseSnapshotAndAllChildren(int licenseId)
        {
            //get license
            var license = _snapshotLicenseRepository.GetSnapshotLicenseByCloneLicenseId(licenseId);

            //Delete License Product List and children
            DeleteLicenseProductAndChildEntities(license);

            //Delete license
            return _snapshotLicenseRepository.DeleteSnapshotLicense(license);
        }

        private void DeleteLicenseProductAndChildEntities(Snapshot_License license)
        {
            //getAllLicenseProducts
            var licenseProducts =
                _snapshotLicenseProductRepository.GetAllLicenseProductsForLicenseId(license.CloneLicenseId);

            //Massive delete

            foreach (var licenseProduct in licenseProducts)
            {
                //Delete All RecsConfiguration and children
                DeleteSnapshotRecsRecordingandChildren(licenseProduct);

                //Delete worksRecording and Children
                DeleteAllWorksRecordingAndChildren(licenseProduct);

                //Delete Product HEader and children
                DeleteProductHeaderAndChildren(licenseProduct);

                //Delete License Product
                _snapshotLicenseProductRepository.DeleteLicenseProductSnapshot(licenseProduct.SnapshotLicenseProductId);
            }
        }

        private void DeleteProductHeaderAndChildren(Snapshot_LicenseProduct licenseProduct)
        {
            if (licenseProduct.ProductHeaderId != null)
            {
                var id = (int)licenseProduct.ProductHeaderId;

                var productHeader =
                    _snapshotProductHeaderRepository.GetProductHeaderByProductHeaderId(
                        id);

                if (productHeader != null)
                {
                    //Delete artist
                    var artist =
                        _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(productHeader.ArtistRecsId);
                    if (artist != null)
                    {
                        _snapshotArtistRecsRepository.DeleteRecsArtisByArtistSnapshotId(
                            artist.SnapshotArtistRecsId);
                    }
                    //Delete label and child (label group)
                    DeleteLabelAndAllChuldren(productHeader);

                    //Delete RecsConfigurations 'Configurations'
                    DeleteAllRecsConfigAndChildrenForProductHeader(productHeader);

                    _snapshotProductHeaderRepository.DeleteProductHeaderSnapshotBySnapshotId(
                        productHeader.SnapshotProductHeaderId);
                }
            }
        }

        private void DeleteLabelAndAllChuldren(Snapshot_ProductHeader productHeader)
        {
            if (productHeader.LabelId > 0)
            {
                var label = _snapshotLabelRepository.GetSnapshotLabelByLabelId(productHeader.LabelId);

                //labelGroups
                var labelGroups =
                    _snapshotLabelGroupRepository.GetAllALabelGroupsForLabelId(
                        label.CloneLabelId);
                if (labelGroups != null)
                {
                    foreach (var labelGroup in labelGroups)
                    {
                        _snapshotLabelGroupRepository.DeleteLabelGroupByLabelGroupSnapshotId(
                            labelGroup.SnapshotLabelGroupId);
                    }
                }

                //Delete label
                _snapshotLabelRepository.DeleteLabelSnapshotBySnapshotId(label.SnapshotLabelId);
            }
        }

        private void DeleteAllRecsConfigAndChildrenForProductHeader(Snapshot_ProductHeader productHeader)
        {
            var recConfigs =
                _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForProductHeaderId(
                    productHeader.CloneProductHeaderId);
            if (recConfigs != null)
            {
                foreach (var config in recConfigs)
                {
                    if (config.ConfigurationId != null)
                    {
                        var id = (int)config.ConfigurationId;
                        //Delete Config
                        var config2 =
                            _snapshotConfigurationRepository.GetSnapshotConfigurationByConfigurationId(id);
                        _snapshotConfigurationRepository.DeleteConfigurationSnapshot(config2.SnapshotConfigId);
                    }

                    //Delete LicenseProductConfig if exists
                    if (config.LicenseProductConfigurationId != null)
                    {
                        var id = (int)config.LicenseProductConfigurationId;

                        /* Mechs data, do not delete
                        var licenseProductConfig =
                            _licenseProductConfigurationRepository
                                .GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
                                    id);

                        //Delete licenseProductConfiguration
                        _licenseProductConfigurationRepository.DeleteLicenseProductConfigurationBySnapshot(licenseProductConfig);
                        */
                    }

                    //delete recConfig
                    _snapshotRecsConfigurationRepository.DeleteWorkRecordingByRecordignSnapshotId(
                        config.SnapshotRecsConfigurationId);
                }
            }
        }

        private void DeleteAllWorksRecordingAndChildren(Snapshot_LicenseProduct licenseProduct)
        {
            if (licenseProduct.CloneLicenseProductId != null)
            {
                var workRecordings =
                    _snapshotWorksRecordingRepository.GetAllWorksRecordingsForLicenseProductId(
                        licenseProduct.CloneLicenseProductId);
                if (workRecordings != null)
                {
                    foreach (var recording in workRecordings)
                    {
                        //delete works track
                        var track = _snapshotWorkTrackRepository.GetTrackForCloneTrackId(recording.TrackId);
                        if (track != null)
                        {
                            //get artist and delte
                            var artist =
                                _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(track.ArtistRecsId);
                            if (artist != null)
                            {
                                _snapshotArtistRecsRepository.DeleteRecsArtisByArtistSnapshotId(
                                    artist.SnapshotArtistRecsId);
                            }

                            //delete copyrights
                            var copyrights =
                                _snapshotRecsCopyrightRespository.GetAllRecsCopyrightsForCloneTrackId(
                                    track.CloneWorksTrackId);

                            if (copyrights != null)
                            {
                                foreach (var copyRight in copyrights)
                                {
                                    //get all samples
                                    var samples =
                                        _snapshotSampleRepository.GetAllSamplesForRecCopyrightId(
                                            copyRight.SnapshotRecsCopyrightsId);

                                    if (samples != null)
                                    {
                                        foreach (var sample in samples)
                                        {
                                            _snapshotSampleRepository.DeleteSampleSnapshot(sample);
                                        }
                                    }
                                    //get all composers
                                    var composers =
                                        _snapshotComposerRepository.GetAllComposersByRecsCopyrightid(
                                            copyRight.SnapshotRecsCopyrightsId);

                                    if (composers != null)
                                    {
                                        foreach (var composer in composers)
                                        {
                                            var affiliations =
                                                _composerAffiliationRepository
                                                    .GetAllComposersAffiliationsByComposerSnapshotId(
                                                        composer.SnapshotComposerId);
                                            if (affiliations != null)
                                            {
                                                foreach (var affiliation in affiliations)
                                                {
                                                    var affiliationBases =
                                                        _composerAffiliationBaseRepository
                                                            .GetAllComposersAffiliationBasesByComposerAffiliationSnapshotId
                                                            (
                                                                affiliation.SnapshotComposerAffiliationId);

                                                    if (affiliationBases != null)
                                                    {
                                                        foreach (var affiliationBase in affiliationBases)
                                                        {
                                                            _composerAffiliationBaseRepository
                                                                .DeleteComposerAffiliationBaseSnapshotByComposer(
                                                                    affiliationBase);
                                                        }
                                                    }
                                                }
                                                foreach (var affiliation in affiliations)
                                                {
                                                    _composerAffiliationRepository
                                                        .DeleteComposerAffiliationSnapshotByComposer(
                                                            affiliation);
                                                }
                                            }

                                            var originalPublishers =
                                                _composerOriginalPublisherRepository
                                                    .GetAllComposerOriginalPublishersForComposerId(
                                                        composer.SnapshotComposerId);
                                            if (originalPublishers != null)
                                            {
                                                foreach (var originalPublisher in originalPublishers)
                                                {
                                                    var admins =
                                                        _composerOriginalPublisherAdministratorRepository
                                                            .GetAllComposerOriginalPublisherAdministratorsForComposerOriginalPublisherId
                                                            (originalPublisher.SnapshotComposerOriginalPublisherId);

                                                    if (admins != null)
                                                    {
                                                        foreach (var admin in admins)
                                                        {
                                                            var adminAffiliations =
                                                                _composerOriginalPublisherAdminAffiliation
                                                                    .GetAllComposerOriginalPublisherAdminAffiliationsorAdminId
                                                                    (
                                                                        admin
                                                                            .SnapshotComposerOriginalPublisherAdministratorId);
                                                            if (adminAffiliations != null)
                                                            {
                                                                foreach (var adminAffiliation in adminAffiliations)
                                                                {
                                                                    var adminBasse =
                                                                        _composerOriginalPublisherAdminAffiliationBaseRepository
                                                                            .GetAllComposerOriginalPublisherAdminAffiliationBasesForAffiliationId
                                                                            (adminAffiliation
                                                                                .SnapshotComposerOriginalPublisherAdminAffiliationId);
                                                                    if (adminBasse != null)
                                                                    {
                                                                        foreach (var adminBase in adminBasse)
                                                                        {
                                                                            _composerOriginalPublisherAdminAffiliationBaseRepository
                                                                                .DeleteComposerOriginalPublisherAdminAffiliationBase
                                                                                (adminBase);
                                                                        }
                                                                    }
                                                                    _composerOriginalPublisherAdminAffiliation
                                                                        .DeleteComposerOriginalPublisherAdminAffiliation
                                                                        (
                                                                            adminAffiliation);
                                                                }
                                                            }

                                                            var compOriginalPublisherAffilaitions =
                                                                _composerOriginalPublisherAffiliationRepository
                                                                    .GetAllComposerOriginalPublisherAffiliationsForComposerOriginalPublisherId
                                                                    (originalPublisher
                                                                        .SnapshotComposerOriginalPublisherId);

                                                            if (compOriginalPublisherAffilaitions != null)
                                                            {
                                                                foreach (
                                                                    var compOriginalPublisherAffilaition in
                                                                    compOriginalPublisherAffilaitions)
                                                                {
                                                                    var compOpAffBase =
                                                                        _composerOriginalPublisherAffiliationBaseRepository
                                                                            .GetComposerOriginalPublisherAffiliationBasesFComposerOriginalPublisherAffiliationId
                                                                            (compOriginalPublisherAffilaition
                                                                                .SnapshotComposerOriginalPublisherAffiliationId);
                                                                    if (compOpAffBase != null)
                                                                    {
                                                                        foreach (var affBase in compOpAffBase)
                                                                        {
                                                                            _composerOriginalPublisherAffiliationBaseRepository
                                                                                .DeleteComposerOriginalPublisherAffiliationBase
                                                                                (
                                                                                    affBase);
                                                                        }
                                                                    }
                                                                    _composerOriginalPublisherAffiliationRepository
                                                                        .DeleteComposerOriginalPubhliserAffiliation(
                                                                            compOriginalPublisherAffilaition);
                                                                }
                                                            }

                                                            _composerOriginalPublisherAdministratorRepository
                                                                .DeleteComposerOriginalPublisherAdministrator(admin);
                                                        }
                                                    }

                                                    var knownAs =
                                                        _composerOriginalPublisherKnownAsRepository
                                                            .GetAllComposerOriginalPublisherKnownAsByComposerOriginalPublisherSnapshotId
                                                            (composer.SnapshotComposerId);

                                                    if (knownAs != null)
                                                    {
                                                        foreach (var snapshotComposerOriginalPublisherKnownAs in knownAs
                                                        )
                                                        {
                                                            _composerOriginalPublisherKnownAsRepository
                                                                .DeleteComposerOriginalPublisherKnownAs(
                                                                    snapshotComposerOriginalPublisherKnownAs);
                                                        }
                                                    }
                                                    _composerOriginalPublisherRepository.DeleteComposerOriginalPublisher
                                                    (
                                                        originalPublisher);
                                                }
                                            }

                                            var localClients =
                                                _snapshotLocalClientCopyrightRepository.GetAllLocalCopyrightsForTrackId(
                                                    track.CloneWorksTrackId);
                                            if (localClients != null)
                                            {
                                                foreach (var localClient in localClients)
                                                {
                                                    _snapshotLocalClientCopyrightRepository
                                                        .DeleteLocalClientCopyrightBySnapshotId(
                                                            localClient.SnapshotLocalClientCopyrightId);
                                                }
                                            }

                                            var locationCodes =
                                                _aquisitionLocationCodeRepository
                                                    .GetAllAquisitionLocationCodesForTrackId(
                                                        track.CloneWorksTrackId);
                                            if (locationCodes != null)
                                            {
                                                foreach (var locationCode in locationCodes)
                                                {
                                                    _aquisitionLocationCodeRepository
                                                        .DeleteAquisitionLocationCodeBySnashotId(
                                                            locationCode.SnapshotAquisitionLocationCode);
                                                }
                                            }

                                            _snapshotRecsCopyrightRespository
                                                .DeleteRecsCopyrightByRecsCopyrightSnapshotId(
                                                    copyRight.SnapshotRecsCopyrightsId);
                                            _snapshotComposerRepository.DeleteComposerSnapshotByComposer(composer);
                                        }
                                    }

                                    //Delete worksRecordiing
                                    _snapshotWorksRecordingRepository.DeleteWorkRecordingByRecordignSnapshotId(
                                        recording.SnapshotWorksRecodingId);
                                }
                            }
                        }
                        if (track != null && track.SnapshotWorkTrackId != null)
                        {
                            _snapshotWorkTrackRepository.DeleteTrackBySnapshotTrackId(track.SnapshotWorkTrackId);
                        }
                        if (recording != null)
                        {
                            DeleteWorksWritersAndChildren(recording);
                        }
                    }
                }
            }
        }

        private void DeleteWorksWritersAndChildren(Snapshot_WorksRecording recording)
        {
            var writersForRecording = _snapshotWorksWriterRepository.GetAllWritersForCloneTrackId(recording.CloneTrackId);

            foreach (var writer in writersForRecording)
            {
                //Delete all affiliation list and child if not null
                //delete affil;iatio base list
                DeleteAllAffiliationsAndChildren(writer);

                //Delete knownAs list if not null
                var knownAsAll = _snapshotKnownAsRepository.GetAllKnownAsForWriterCaeCode(writer.CloneCaeNumber);
                if (knownAsAll != null)
                {
                    foreach (var knownAs in knownAsAll)
                    {
                        _snapshotKnownAsRepository.DeleteKnownAsBySnapshotId(knownAs.SnapshotKnownAsId);
                    }
                }
                //Delete Original Publiliser list and child if not null
                DeleteOriginalPublisherSnapshotAndChildern(writer);

                //delete writer
                _snapshotWorksWriterRepository.DeleteWorksWriterSnapshotBySnapshotId(writer.SnapshotWorksWriterId);
            }
        }

        private void DeleteOriginalPublisherSnapshotAndChildern(Snapshot_WorksWriter writer)
        {
            var allOriginalPublishers =
                _snapshotOriginalPublisherRepository.GetAllOriginalPublishersForSnapshotWriterId(writer.SnapshotWorksWriterId);

            foreach (var originalPublisher in allOriginalPublishers)
            {
                var affiliationList =
                    _originalPublisherAffiliationRepository.GetAllOriginalPublisherAffiliationsByOriginalBuplisherId(
                        originalPublisher.SnapshotOriginalPublisherId);
                if (affiliationList != null)
                {
                    foreach (var affiliation in affiliationList)
                    {
                        var affiliationBases =
                            _originalPubAffiliationBaseRepository.GetAllOriginalPubAffiliationBasesByAffilationId(
                                affiliation.SnapshotOriginalPublisherAffiliationId);
                        if (affiliationBases != null)
                        {
                            foreach (var affiliationBase in affiliationBases)
                            {
                                _originalPubAffiliationBaseRepository.DeletePhoneBySnapshotPhoneId(
                                    affiliationBase.SnapshotOriginalPubAffiliationBaseId);
                            }
                        }
                        _originalPublisherAffiliationRepository.DeleteOriginalPublisherSnapshotById(
                            affiliation.SnapshotOriginalPublisherAffiliationId);
                    }
                }

                var admins =
                    _snapshotAdministratorRepository.GetAllAdministratorsForOriginalPublisherId(
                        originalPublisher.SnapshotOriginalPublisherId);

                if (admins != null)
                {
                    foreach (var admin in admins)
                    {
                        var adminAffiliations =
                            _adminAffiliationRepository.GetAllAdminAffiliationsForSnapshotAdminId(
                                admin.SnapshotAdministratorId);
                        if (adminAffiliations != null)
                        {
                            foreach (var adminAffiliation in adminAffiliations)
                            {
                                var adminAFfiliationBase =
                                    _adminAffiliationBaseRepository.GetAllAdminAffiliationBaseForSnapshotAdminId(
                                        adminAffiliation.SnapshotAdminAffiliationId);
                                if (adminAFfiliationBase != null)
                                {
                                    foreach (var adminBase in adminAFfiliationBase)
                                    {
                                        _adminAffiliationBaseRepository.DeleteConfigurationSnapshot(
                                            adminBase.SnapshotAdminAffiliationBaseId);
                                    }
                                }
                                _adminAffiliationRepository.DeletePhoneBySnapshotPhoneId(
                                    adminAffiliation.SnapshotAdminAffiliationId);
                            }
                        }
                        _snapshotAdministratorRepository.DeleteConfigurationSnapshot(admin.SnapshotAdministratorId);
                    }
                }

                var knownAsList =
                    _snapshotKnownAsRepository.GetAllKnownAsForWriterCaeCode(
                        originalPublisher.CloneCaeNumber);
                if (knownAsList != null)
                {
                    foreach (var knownAs in knownAsList)
                    {
                        _snapshotKnownAsRepository.DeleteKnownAsBySnapshotId(knownAs.SnapshotKnownAsId);
                    }
                }
                _snapshotOriginalPublisherRepository.DeleteOriginalPublisherSnapshotBySnapshotId(
                    originalPublisher.SnapshotOriginalPublisherId);
            }
        }

        private void DeleteAllAffiliationsAndChildren(Snapshot_WorksWriter writer)
        {
            var allAffiliations = _snapshotAffiliationRepository.GetAllAffiliationsForWriterSnapshotId(writer.SnapshotWorksWriterId);
            if (allAffiliations != null)
            {
                foreach (var affilation in allAffiliations)
                {
                    var baseAffiliations =
                        _affiliationBaseRepository.GetAllAffiliationBasesForAffilationId(
                            affilation.SnapshotAffiliationId);
                    if (baseAffiliations != null)
                    {
                        foreach (var baseAffiliation in baseAffiliations)
                        {
                            _affiliationBaseRepository.DeleteAffilationByAffiliationBaseSnapshotId(
                                baseAffiliation.SnapshotAffiliationBaseId);
                        }
                    }

                    _snapshotAffiliationRepository.DeleteAffilationByAffiliationSnapshotId(
                        affilation.SnapshotAffiliationId);
                }
            }
        }

        private void DeleteSnapshotRecsRecordingandChildren(Snapshot_LicenseProduct licenseProduct)
        {
            var licenseConfigurations =
                _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForLicenseProductId(licenseProduct.CloneLicenseProductId);
            if (licenseConfigurations != null)
            {
                //Delete all children
                foreach (var licenseConfig in licenseConfigurations)
                {
                    //Delete Config
                    _snapshotConfigurationRepository.DeleteConfigurationSnapshot(licenseConfig.SnapshotConfigurationId);
                }

                var ids = licenseConfigurations.Select(_ => _.SnapshotRecsConfigurationId).ToList();
                foreach (var id in ids)
                {
                    //delete recConfig
                    _snapshotRecsConfigurationRepository.DeleteWorkRecordingByRecordignSnapshotId(
                        id);
                }
            }
        }
    }
}